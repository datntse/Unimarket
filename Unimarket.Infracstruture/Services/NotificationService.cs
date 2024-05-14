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
    public interface INotificationService
    {
        Task<IdentityResult> CreateNoti(NotiDTO noti);
        Task<Notification> FindAsync(Guid id);
        IQueryable<Notification> GetAll();
        IQueryable<Notification> Get(Expression<Func<Notification, bool>> where);
        IQueryable<Notification> Get(Expression<Func<Notification, bool>> where, params Expression<Func<Notification, object>>[] includes);
        IQueryable<Notification> Get(Expression<Func<Notification, bool>> where, Func<IQueryable<Notification>, IIncludableQueryable<Notification, object>> include = null);
        Task AddAsync(Notification noti);
        Task AddRangce(IEnumerable<Notification> Notifications);
        void Update(Notification noti);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<Notification, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    public class NotificationService : INotificationService
    {
        private IUnitOfWork _unitOfWork;
        private INotificationRepository _notificationRepository;

        public NotificationService(IUnitOfWork unitOfWork, INotificationRepository notificationRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
        }

        public async Task AddAsync(Notification noti)
        {
            await _notificationRepository.AddAsync(noti);
        }

        public Task AddRangce(IEnumerable<Notification> Notifications)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExist(Expression<Func<Notification, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateNoti(NotiDTO noti)
        {
            var newNoti = new Notification
            {
                Title = noti.Title,
                Description = noti.Description,
                CreateAt = DateTime.Now,
                IsRead = false,
                notiStatus = noti.Status
            };
            await _notificationRepository.AddAsync(newNoti);
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

        public async Task<Notification> FindAsync(Guid id)
        {
            return await _notificationRepository.FindAsync(id);
        }

        public IQueryable<Notification> Get(Expression<Func<Notification, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Notification> Get(Expression<Func<Notification, bool>> where, params Expression<Func<Notification, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Notification> Get(Expression<Func<Notification, bool>> where, Func<IQueryable<Notification>, IIncludableQueryable<Notification, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _unitOfWork.SaveChangeAsync();
        }

        public void Update(Notification noti)
        {
            throw new NotImplementedException();
        }
    }
}
