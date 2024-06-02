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
    public interface IItemImageService
    {
        IQueryable<ItemImage> GetAll();
        IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where);
        IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where, params Expression<Func<ItemImage, object>>[] includes);
        IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where, Func<IQueryable<ItemImage>, IIncludableQueryable<ItemImage, object>> include = null);
        Task AddRangce(IEnumerable<ItemImage> Items);
        void Update(ItemImage x);
        Task<bool> Remove(Guid id);
        Task<bool> SaveChangeAsync();
    }

    public class ItemImageService : IItemImageService
    {
        private IUnitOfWork _unitOfWork;
        private IItemImageRepository _itemImageRepository;

        public ItemImageService(IUnitOfWork unitOfWork, IItemImageRepository itemImageRepository)
        {
            _unitOfWork = unitOfWork;
            _itemImageRepository = itemImageRepository;
        }

        public async Task AddAsync(ItemImage x)
        {
            await _itemImageRepository.AddAsync(x);
        }

        public async Task AddRangce(IEnumerable<ItemImage> Items)
        {
            await _itemImageRepository.AddRangce(Items);   
        }


        public  IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where)
        {
            return  _itemImageRepository.Get(where);
        }

        public IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where, params Expression<Func<ItemImage, object>>[] includes)
        {
            return _itemImageRepository.Get(where, includes);
        }

        public IQueryable<ItemImage> Get(Expression<Func<ItemImage, bool>> where, Func<IQueryable<ItemImage>, IIncludableQueryable<ItemImage, object>> include = null)
        {
            return _itemImageRepository.Get(where, include);
        }

        public IQueryable<ItemImage> GetAll()
        {
            return _itemImageRepository.GetAll();
        }

        public Task<bool> Remove(Guid id)
        {
            return _itemImageRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(ItemImage x)
        {
            _itemImageRepository.Update(x);
        }
    }
}
