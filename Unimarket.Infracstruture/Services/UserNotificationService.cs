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
    public interface IUserNotificationService
    {
        Task<IdentityResult> CreateUserNoti(UserNotiDTO model);
        Task<UserNotification> FindAsync(Guid id);
        IQueryable<UserNotification> GetAll();
        IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where);
        IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where, params Expression<Func<UserNotification, object>>[] includes);
        IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where, Func<IQueryable<UserNotification>, IIncludableQueryable<UserNotification, object>> include = null);
        Task AddAsync(UserNotification noti);
        Task AddRangce(IEnumerable<UserNotification> userNotifications);
        void Update(UserNotification noti);
        Task<bool> Remove(Guid id);
        Task<bool> CheckExist(Expression<Func<UserNotification, bool>> where);
        Task<bool> SaveChangeAsync();
    }
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserNotificationRepository _userNotiRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserNotificationService(IUnitOfWork unitOfWork, IUserNotificationRepository userNotiRepository, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userNotiRepository = userNotiRepository;
            _userManager = userManager;
        }

        public Task AddAsync(UserNotification noti)
        {
            throw new NotImplementedException();
        }

        public Task AddRangce(IEnumerable<UserNotification> userNotifications)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckExist(Expression<Func<UserNotification, bool>> where)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateUserNoti(UserNotiDTO model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.userId);

                if (user == null)
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "UserNotFound",
                        Description = "User not found"
                    });
                }

                var newUserNotification = new UserNotification
                {
                    ItemId = model.ItemId,
                    PostId = model.PostId,
                    NotificationId = model.NotificationId,
                    UserIdFor = model.UserIdFor,
                    User = user,
                };

                await _userNotiRepository.AddAsync(newUserNotification);
                var saveResult = await _unitOfWork.SaveChangeAsync();  // Assuming SaveChangeAsync is an asynchronous method

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
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }
        }


        public Task<UserNotification> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where, params Expression<Func<UserNotification, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserNotification> Get(Expression<Func<UserNotification, bool>> where, Func<IQueryable<UserNotification>, IIncludableQueryable<UserNotification, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserNotification> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(UserNotification noti)
        {
            throw new NotImplementedException();
        }
    }
}
