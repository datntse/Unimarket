using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Constants;
using Unimarket.Core.Entities;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;

namespace Unimarket.Infracstruture.Services
{
    public interface ICartService
    {
        Task<IdentityResult> AddToCart(string userId, AddItemDTO AddItem);
        Task<IdentityResult> UpdateItemQuantity(string userId, UpdateItemQuantityDTO updateItem);
        Task<IdentityResult> AddQuantityToCart(string userId, UpdateItemQuantityDTO addItem);
        //Task<List<CartItem>> GetCartItemsByUserId(string userId);
        IQueryable<CartDTO> GetCartItemsByUserId(string userId);
        Task<IdentityResult> DeleteItemInCart(string userId, AddItemDTO deleteItem);
        Task<CartItem> FindAsync(Guid id);
        IQueryable<CartItem> GetAll();
        IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where);
        IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where, params Expression<Func<CartItem, object>>[] includes);
        IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where, Func<IQueryable<CartItem>, IIncludableQueryable<CartItem, object>> include = null);
        Task AddAsync(CartItem x);
        Task AddRangce(IEnumerable<CartItem> CartItems);
        void Update(CartItem x);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<CartItem, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    internal class CartService : ICartService
    {
        private IUnitOfWork _unitOfWork;
        private ICartRepository _cartRepository;
        private IItemRepository _itemRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(IUnitOfWork unitOfWork, ICartRepository cartRepository,IItemRepository itemRepository, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        public async Task AddAsync(CartItem x)
        {
            await _cartRepository.AddAsync(x);
        }

        public Task AddRangce(IEnumerable<CartItem> CartItems)
        {
            throw new NotImplementedException();
        }
        public async Task<List<CartItem>> GetCartItemsByUserId(string userId)
        {
            return await _cartRepository.Get(c => c.User.Id == userId).ToListAsync();
        }
        public async Task<IdentityResult> AddToCart(string userId ,AddItemDTO AddItem)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var items = await _itemRepository.FindAsync(AddItem.ItemId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User does not exist." });
            }
            var existingCartItem = _cartRepository.Get(c => c.User.Id == userId && c.ItemId == AddItem.ItemId).FirstOrDefault();
            if (existingCartItem != null)
            {
                // Item exists, update the quantity
                existingCartItem.Quantity += 1;
                existingCartItem.UpdateAt = DateTime.UtcNow;
                _cartRepository.Update(existingCartItem);
            }
            else
            {
                // Item does not exist, add a new CartItem
                var newCartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Items = items,
                    Quantity = 1,
                    UpdateAt = DateTime.UtcNow,
                    CreateAt = DateTime.UtcNow,
                };
                await _cartRepository.AddAsync(newCartItem);
            }
            var saveResult = await _unitOfWork.SaveChangeAsync();
            if (saveResult)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Could not save changes to the database." });

        }

        public async Task<IdentityResult> UpdateItemQuantity(string userId,UpdateItemQuantityDTO updateItem)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }


            var cartItems = _cartRepository
            .Get(c => c.User.Id == userId)
            .Include(c=>c.Items).ToList();

            // Check if the item exists in the user's cart
            var existingCartItem = _cartRepository.Get(c => c.User.Id == userId && c.ItemId == updateItem.ItemId).FirstOrDefault();

            if (existingCartItem == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Item not found in cart." });
            }

            if (updateItem.Quantity <= 0)
            {
                _cartRepository.Remove(existingCartItem);
            }
            else
            {
                existingCartItem.Quantity = updateItem.Quantity;
                _cartRepository.Update(existingCartItem);
            }

            // Save the changes to the database
            var saveResult = await _unitOfWork.SaveChangeAsync();
            if (saveResult)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Could not save changes to the database." });
        }
        public async Task<IdentityResult> DeleteItemInCart(string userId, AddItemDTO deleteItem)
        {
            var cartItem = await _cartRepository.Get(c => c.User.Id == userId && c.ItemId == deleteItem.ItemId).FirstOrDefaultAsync();
            if (cartItem == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Item not found in cart." });
            }

            _cartRepository.Remove(cartItem);

            var saveResult = await _unitOfWork.SaveChangeAsync();
            if (saveResult)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Could not remove item from the cart." });
        }

        public Task<bool> CheckExist(Expression<Func<CartItem, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where)
        {
            return _cartRepository.Get(where); ;
        }

        public IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where, params Expression<Func<CartItem, object>>[] includes)
        {
            return _cartRepository.Get(where, includes);
        }

        public IQueryable<CartItem> Get(Expression<Func<CartItem, bool>> where, Func<IQueryable<CartItem>, IIncludableQueryable<CartItem, object>> include = null)
        {
            return _cartRepository.Get(where, include);
        }

        public IQueryable<CartItem> GetAll()
        {
            return _cartRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _cartRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(CartItem x)
        {
            _cartRepository.Update(x);
        }

        public async Task<IdentityResult> AddQuantityToCart(string userId,UpdateItemQuantityDTO addItem)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User does not exist." });
            }

            var item = await _itemRepository.FindAsync(addItem.ItemId);
            if (item == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Item does not exist." });
            }

            var existingCartItem = _cartRepository.Get(c => c.User.Id == userId && c.ItemId == addItem.ItemId).FirstOrDefault();
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addItem.Quantity;
                existingCartItem.UpdateAt = DateTime.UtcNow;

                if (existingCartItem.Quantity <= 0)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Quantity must be greater than 0." });
                }
                else
                {
                    _cartRepository.Update(existingCartItem);
                }
            }
            else
            {
                // Item does not exist, add a new CartItem if the quantity is positive
                if (addItem.Quantity > 0)
                {
                    var newCartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        ItemId = addItem.ItemId,
                        User = user,
                        Quantity = addItem.Quantity,
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                    };
                    await _cartRepository.AddAsync(newCartItem);
                }
            }

            var saveResult = await _unitOfWork.SaveChangeAsync();
            if (saveResult)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Description = "Could not save changes to the database." });
        }

        IQueryable<CartDTO> ICartService.GetCartItemsByUserId(string userId)
        {
            return _cartRepository.Get(c => c.User.Id == userId)
                                   .Select(c => new CartDTO
                                   {
                                       Id = c.Id,
                                       ItemId = c.ItemId,
                                       userId = c.User.Id,
                                       CreateAt = c.CreateAt,
                                       UpdateAt = c.UpdateAt,
                                       Quantity = c.Quantity,
                                       Item = new ItemCartVM
                                       {
                                           Name = c.Items.Name,
                                           Description = c.Items.Description,
                                           Price = c.Items.Price,
                                           ImageUrl = c.Items.ImageUrl
                                       }
                                   });
        }
    }
}
