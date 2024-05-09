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
    public interface ICategoryService
    {
        Task<Category> FindAsync(Guid id);
        IQueryable<Category> GetAll();
        IQueryable<Category> Get(Expression<Func<Category, bool>> where);
        IQueryable<Category> Get(Expression<Func<Category, bool>> where, params Expression<Func<Category, object>>[] includes);
        IQueryable<Category> Get(Expression<Func<Category, bool>> where, Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null);
        Task AddAsync(Category category);
        Task AddRangce(IEnumerable<Category> categorys);
        void Update(Category category);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Category, bool>> where);
        Task<bool> SaveChangeAsync();
    }

    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private ICategoryRepository _categoryRepository;
        public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _unitOfWork = unitOfWork;   
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }

        public async Task AddRangce(IEnumerable<Category> categorys)
        {
            await _categoryRepository.AddRangce(categorys);
        }

        public async Task<bool> CheckExist(Expression<Func<Category, bool>> where)
        {
            return await _categoryRepository.CheckExist(where);
        }

        public async Task<Category> FindAsync(Guid id)
        {
            return await _categoryRepository.FindAsync(id);
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> where)
        {
            return _categoryRepository.Get(where);
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> where, params Expression<Func<Category, object>>[] includes)
        {
            return _categoryRepository.Get(where, includes);
        }

        public IQueryable<Category> Get(Expression<Func<Category, bool>> where, Func<IQueryable<Category>, IIncludableQueryable<Category, object>> include = null)
        {
            return _categoryRepository.Get(where, include);
        }

        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _categoryRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}
