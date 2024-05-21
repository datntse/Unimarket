using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        Task<IdentityResult> CheckOut(CheckOutDTO AddItem);
        Task<Order> FindAsync(Guid id);
        IQueryable<Order> GetAll();
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
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderService(
            IUnitOfWork unitOfWork,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CheckOut(CheckOutDTO checkOutDTO)
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
                PaymentType = checkOutDTO.PaymentType,
                TotalPrice = totalPrice,
                User = user,
                Status = 1, 
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

                order.OrderDetails.Add(orderDetail);
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangeAsync();

            // Clear the user's cart after checkout
            _cartRepository.Remove(cartItems); 

            await _unitOfWork.SaveChangeAsync();


            return IdentityResult.Success;
        }



        public async Task AddAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public Task AddRangce(IEnumerable<Order> Orders)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExist(Expression<Func<Order, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where, params Expression<Func<Order, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> Get(Expression<Func<Order, bool>> where, Func<IQueryable<Order>, IIncludableQueryable<Order, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Order> GetAll()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
