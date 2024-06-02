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
    public interface IItemCategoryService
    {
        IQueryable<ItemCategory> GetAll();
        IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where);
        IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where, params Expression<Func<ItemCategory, object>>[] includes);
        IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where, Func<IQueryable<ItemCategory>, IIncludableQueryable<ItemCategory, object>> include = null);
        Task AddRangce(IEnumerable<ItemCategory> Items);
        void Update(ItemCategory x);
        Task<bool> Remove(Guid id);
        Task<bool> SaveChangeAsync();
    }

    public class ItemCategoryService : IItemCategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IItemCategoryRepository _itemCategoryRepository;

        public ItemCategoryService(IUnitOfWork unitOfWork, IItemCategoryRepository itemCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _itemCategoryRepository = itemCategoryRepository;
        }

        public async Task AddRangce(IEnumerable<ItemCategory> Items)
        {
            await _itemCategoryRepository.AddRangce(Items);
        }

        public IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where)
        {
            return _itemCategoryRepository.Get(where);
        }

        public IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where, params Expression<Func<ItemCategory, object>>[] includes)
        {
            return _itemCategoryRepository.Get(where, includes);
        }

        public IQueryable<ItemCategory> Get(Expression<Func<ItemCategory, bool>> where, Func<IQueryable<ItemCategory>, IIncludableQueryable<ItemCategory, object>> include = null)
        {
            return _itemCategoryRepository.Get(where, include);
        }

        public IQueryable<ItemCategory> GetAll()
        {
            return _itemCategoryRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _itemCategoryRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(ItemCategory x)
        {
            _itemCategoryRepository.Update(x);
        }
    }
}
