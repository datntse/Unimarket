using Microsoft.AspNetCore.Identity;
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
    public interface ICartService
    {
        Task<IdentityResult> AddToCart(AddItemDTO AddItem);
        Task<IdentityResult> UpdateItemQuantity(UpdateItemQuantityDTO updateItem);
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CartService(IUnitOfWork unitOfWork, ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _cartRepository = cartRepository;
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

        public async Task<IdentityResult> AddToCart(AddItemDTO AddItem)
        {
            var user = await _userManager.FindByIdAsync(AddItem.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User does not exist." });
            }
            var existingCartItem = _cartRepository.Get(c => c.User.Id == AddItem.UserId && c.ItemId == AddItem.ItemId).FirstOrDefault();
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
                    ItemId = AddItem.ItemId,
                    User = user,
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

        public async Task<IdentityResult> UpdateItemQuantity(UpdateItemQuantityDTO updateItem)
        {
            // Retrieve the user by their userId
            var user = await _userManager.FindByIdAsync(updateItem.UserId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            // Check if the item exists in the user's cart
            var existingCartItem = _cartRepository.Get(c => c.User.Id == updateItem.UserId && c.ItemId == updateItem.ItemId).FirstOrDefault();

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
            throw new NotImplementedException();
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
    }
}
