using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;

namespace Unimarket.Infracstruture.Services
{
    public interface IOrderService
    {
        Task<string> CheckOut(CheckOutDTO AddItem);
		Task<IdentityResult> UpdateOrder(UpdateOrderUM upOrder);
		IQueryable<OrderVM> GetOrdersByUserId(string userId);
        Task<Order> FindAsync(Guid id);
        Task<string> CreateVnPayUrl(float amount, string orderDescription, string locale);
        Task<IdentityResult> ConfirmVnPayPayment(IQueryCollection vnPayResponse);
        IQueryable<OrderVM> GetAll();
        IQueryable<Order> Get(Expression<Func<Order, bool>> where);
        IQueryable<Order> Get(Expression<Func<Order, bool>> where, params Expression<Func<Order, object>>[] includes);
        IQueryable<Order> Get(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null);
        Task AddAsync(Order order);
        Task AddRangce(IEnumerable<Order> Orders);
        void Update(Order order);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Order, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<OrderService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(
            IUnitOfWork unitOfWork,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager,
            IItemRepository itemRepository,
            IPaymentService paymentService,
            ILogger<OrderService> logger,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
            _itemRepository = itemRepository;
            _paymentService = paymentService;
            _logger = logger;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CheckOut(CheckOutDTO checkOutDTO)
        {
            var user = await _userManager.FindByIdAsync(checkOutDTO.UserId);
            var cartItems = await _cartRepository.Get(c => c.User.Id == checkOutDTO.UserId).Include(c=>c.Items).ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                throw new ApplicationException("Cart is empty.");
            }

            var totalPrice = cartItems.Sum(c => c.Quantity * c.Items.Price);

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Address = checkOutDTO.Address,
                PaymentType = checkOutDTO.PaymentType,
                TotalPrice = totalPrice,
                User = user,
                Status = 0, 
                CreateAt = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var cartItem in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ItemId = cartItem.ItemId,
                    Quantity = cartItem.Quantity,
                    TotalPrice = cartItem.Quantity * cartItem.Items.Price
                };
                var item = await _itemRepository.FindAsync(cartItem.ItemId);
                item.Quantity -= cartItem.Quantity;
                _itemRepository.Update(item);
                await _unitOfWork.SaveChangeAsync();
                order.OrderDetails.Add(orderDetail);
            }

            if (checkOutDTO.PaymentType == "vnpay")
            {
                await _orderRepository.AddAsync(order);
                await _unitOfWork.SaveChangeAsync();
                var paymentUrl = _paymentService.CreatePaymentUrl(totalPrice, "Order Payment", "vn");
                return paymentUrl;
            }
            else
            {
                await _orderRepository.AddAsync(order);
                await _unitOfWork.SaveChangeAsync();

                // Clear the user's cart after checkout
                _cartRepository.Remove(cartItems);
                await _unitOfWork.SaveChangeAsync();

                return null;
            }
        }

        public async Task<string> CreateVnPayUrl(float amount, string orderDescription, string locale)
        {
            return _paymentService.CreatePaymentUrl(amount, orderDescription, locale);
        }

        public async Task AddAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task<IdentityResult> ConfirmVnPayPayment(IQueryCollection vnPayResponse)
        {
            _logger.LogInformation("Begin VNPAY Return, URL={0}", vnPayResponse.ToString());

            string vnp_HashSecret = _configuration["VnPay:HashSecret"];
            var vnpay = new VnPayLibrary();

            foreach (var key in vnPayResponse.Keys)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, vnPayResponse[key]);
                }
            }

            if (!Guid.TryParse(vnpay.GetResponseData("vnp_TxnRef"), out Guid orderId))
            {
                _logger.LogError("Invalid order ID format in vnp_TxnRef");
                return IdentityResult.Failed(new IdentityError { Description = "Invalid order ID format" });
            }

            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnPayResponse["vnp_SecureHash"];
            string terminalID = vnPayResponse["vnp_TmnCode"];
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            string bankCode = vnPayResponse["vnp_BankCode"];

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                var order = await _orderRepository.FindAsync(orderId);
                if (order == null)
                {
                    _logger.LogError("Order not found for ID {0}", orderId);
                    return IdentityResult.Failed(new IdentityError { Description = "Order not found" });
                }

                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    // Payment successful
                    order.Status = 1; // Update order status to successful
                    _orderRepository.Update(order);
                    await _unitOfWork.SaveChangeAsync();

                    // Clear the user's cart after successful payment
                    var cartItems = await _cartRepository.Get(c => c.User.Id == order.User.Id).ToListAsync();
                    _cartRepository.Remove(cartItems);
                    await _unitOfWork.SaveChangeAsync();

                    _logger.LogInformation("Payment successful, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                    return IdentityResult.Success;
                }
                else
                {
                    // Payment failed
                    _logger.LogError("Payment failed, OrderId={0}, VNPAY TranId={1}, ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    return IdentityResult.Failed(new IdentityError { Description = "Payment failed with response code: " + vnp_ResponseCode });
                }
            }
            else
            {
                _logger.LogError("Invalid signature, InputData={0}", vnPayResponse.ToString());
                return IdentityResult.Failed(new IdentityError { Description = "Invalid signature" });
            }
        }
        public Task AddRangce(IEnumerable<Order> Orders)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExist(Expression<Func<Order, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> FindAsync(Guid id)
        {
            return await _orderRepository.FindAsync(id);
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where)
        {
            return _orderRepository.Get(where);
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where, params Expression<Func<Order, object>>[] includes)
        {
            return _orderRepository.Get(where, includes);
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null)
        {
            return _orderRepository.Get(where, include);
        }

        public IQueryable<OrderVM> GetAll()
        {
            return _orderRepository.Get(o => o.Id != null)
                                    .Include(o => o.User)
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Items)
                                    .Select(order =>
                                    
                                        new OrderVM
                                        {
                                            Id = order.Id,
                                            PaymentType = order.PaymentType,
                                            TotalPrice = order.TotalPrice,
                                            Status = order.Status,
                                            CreateAt = order.CreateAt,
                                            Username = order.User.UserName,
                                            Address = order.Address,
                                            FirstName = order.User.FirstName,
                                            LastName = order.User.LastName,
                                            PhoneNumber = order.User.PhoneNumber,
                                            OrderdetailVM = order.OrderDetails.Select(od => new OrderdetailVM
                                            {
                                                Id = od.Id,
                                                Quantity = od.Quantity,
                                                TotalPrice = od.TotalPrice,
                                                ItemsVMs = new ItemsVM
                                                {
                                                    Name = od.Items.Name,
                                                    Price = od.Items.Price,
                                                    ImageUrl = od.Items.ImageUrl
                                                }
                                            }).ToList()
                                        }
                                    );
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public IQueryable<OrderVM> GetOrdersByUserId(string userId)
{
            return _orderRepository.Get(o => o.User.Id == userId)
                                    .Include(o => o.User)
                                    .Include(o => o.OrderDetails)
                                    .ThenInclude(od => od.Items)
                                    .Select(order =>

                                        new OrderVM
                                        {
                                            Id = order.Id,
                                            PaymentType = order.PaymentType,
                                            TotalPrice = order.TotalPrice,
                                            Status = order.Status,
                                            CreateAt = order.CreateAt,
                                            Username = order.User.UserName,
                                            Address = order.Address,
                                            FirstName = order.User.FirstName,
                                            LastName = order.User.LastName,
                                            PhoneNumber = order.User.PhoneNumber,
                                            OrderdetailVM = order.OrderDetails.Select(od => new OrderdetailVM
                                            {
                                                Id = od.Id,
                                                Quantity = od.Quantity,
                                                TotalPrice = od.TotalPrice,
                                                ItemsVMs = new ItemsVM
                                                {
                                                    Name = od.Items.Name,
                                                    Price = od.Items.Price,
                                                    ImageUrl = od.Items.ImageUrl
                                                }
                                            }).ToList()
                                        }
                                    );
        }

        public async Task<IdentityResult> UpdateOrder(UpdateOrderUM upOrder)
        {
            var order = await _orderRepository.FindAsync(upOrder.OrderId);
            if (order != null)
            {
                order.Status = upOrder.Status;
                _orderRepository.Update(order);
                await _unitOfWork.SaveChangeAsync();
				return IdentityResult.Success;
			}else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Could not save changes to the database." });
			}
        }

	}
}
