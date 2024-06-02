using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;
using Unimarket.Infracstruture.Repositories;

namespace Unimarket.Infracstruture.Services
{
    public interface IItemService
    {
        Task<Item> FindAsync(Guid id);
        IQueryable<Item> GetAll();
        IQueryable<Item> Get(Expression<Func<Item, bool>> where);
        IQueryable<Item> Get(Expression<Func<Item, bool>> where, params Expression<Func<Item, object>>[] includes);
        IQueryable<Item> Get(Expression<Func<Item, bool>> where, Func<IQueryable<Item>, IIncludableQueryable<Item, object>> include = null);
        Task AddAsync(Item x);
        Task AddRangce(IEnumerable<Item> Items);
        void Update(Item x);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Item, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    internal class ItemService : IItemService
    {
        private IUnitOfWork _unitOfWork;
        private IItemRepository _itemRepository;

        public ItemService(IUnitOfWork unitOfWork, IItemRepository itemRepository)
        {
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
        }

        public async Task AddAsync(Item x)
        {
            await _itemRepository.AddAsync(x);
        }

        public async Task AddRangce(IEnumerable<Item> Items)
        {
            await _itemRepository.AddRangce(Items);
        }

        public async Task<bool> CheckExist(Expression<Func<Item, bool>> where)
        {
            return await _itemRepository.CheckExist(where);
        }

        public async Task<Item> FindAsync(Guid id)
        {
            return await _itemRepository.FindAsync(id);
        }

        public IQueryable<Item> Get(Expression<Func<Item, bool>> where)
        {
            return _itemRepository.Get(where);
        }

        public IQueryable<Item> Get(Expression<Func<Item, bool>> where, params Expression<Func<Item, object>>[] includes)
        {
            return _itemRepository.Get(where, includes);
        }

        public IQueryable<Item> Get(Expression<Func<Item, bool>> where, Func<IQueryable<Item>, IIncludableQueryable<Item, object>> include = null)
        {
            return _itemRepository.Get(where, include);   
        }

        public IQueryable<Item> GetAll()
        {
            return _itemRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _itemRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Item x)
        {
            _itemRepository.Update(x);
        }
    }
}
