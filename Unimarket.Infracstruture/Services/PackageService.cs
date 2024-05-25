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
    public interface IPackageService
    {
        Task<IdentityResult> CreatePackage(PackageCM packages);
        Task<IdentityResult> UpdatePackage(PackageUM packages);
        Task<IdentityResult> DeletePackage(Guid id);
        Task<Package> FindAsync(Guid id);
        IQueryable<Package> GetAll();
        IQueryable<Package> Get(Expression<Func<Package, bool>> where);
        IQueryable<Package> Get(Expression<Func<Package, bool>> where, params Expression<Func<Package, object>>[] includes);
        IQueryable<Package> Get(Expression<Func<Package, bool>> where, Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = null);
        Task AddAsync(Package package);
        Task AddRangce(IEnumerable<Package> packages);
        void Update(Package package);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Package, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    public class PackageService : IPackageService
    {
        private IUnitOfWork _unitOfWork;
        private IPackageRepository _packageRepository;

        public PackageService(IUnitOfWork unitOfWork, IPackageRepository packageRepository)
        {
            _unitOfWork = unitOfWork;
            _packageRepository = packageRepository;
        }

        public async Task AddAsync(Package package)
        {
            await _packageRepository.AddAsync(package);
        }

        public Task AddRangce(IEnumerable<Package> packages)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExist(Expression<Func<Package, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreatePackage(PackageCM packages)
        {
            var newPackage = new Package
            {
                Id = Guid.NewGuid(),
                Name = packages.Name,
                Description = packages.Description,
                Quantity = packages.Quantity,
                Price= packages.Price,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
            };
            await _packageRepository.AddAsync(newPackage);
            var saveResult = await _unitOfWork.SaveChangeAsync();
            if (saveResult)
            {
                // Return a successful IdentityResult
                return IdentityResult.Success;
            }
            else
            {
                // Return an IdentityResult with an error message
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "SaveFailed",
                    Description = "Failed to save changes to the database."
                });
            }
        }

        public async Task<IdentityResult> DeletePackage(Guid id)
        {
            var packageToDelete = await _packageRepository.FindAsync(id);
            if (packageToDelete == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "PackageNotFound",
                    Description = "Package not found."
                });
            }
            _packageRepository.Remove(packageToDelete);
            await _unitOfWork.SaveChangeAsync();
            return IdentityResult.Success;

        }

        public async Task<Package> FindAsync(Guid id)
        {
            return await _packageRepository.FindAsync(id);
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where, params Expression<Func<Package, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Package> Get(Expression<Func<Package, bool>> where, Func<IQueryable<Package>, IIncludableQueryable<Package, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Package> GetAll()
        {
            return _packageRepository.GetAll();
        }

        public async Task<bool> Remove(Guid id)
        {
            return await _packageRepository.Remove(id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Package package)
        {
            _packageRepository.Update(package);
        }

        public async Task<IdentityResult> UpdatePackage(PackageUM packages)
        {
            var packageNeedUpdate = await _packageRepository.FindAsync(packages.Id);
            if(packageNeedUpdate == null) {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "PackageNotFound",
                    Description = "Package not found."
                });
            }
            //Update value
            packageNeedUpdate.Name = packages.Name;
            packageNeedUpdate.Description = packages.Description;
            packageNeedUpdate.Price = packages.Price;
            packageNeedUpdate.Quantity = packages.Quantity;
            packageNeedUpdate.UpdateAt = DateTime.Now;

            _packageRepository.Update(packageNeedUpdate);
            await _unitOfWork.SaveChangeAsync();
            return IdentityResult.Success;
        }
    }
}
