using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface IUserNotificationRepository : IBaseRepository<UserNotification, Guid>
    {
    }
    public class UserNotificationRepository : BaseRepository<UserNotification,Guid>, IUserNotificationRepository
    {
        public UserNotificationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
        }
    }
    
}
