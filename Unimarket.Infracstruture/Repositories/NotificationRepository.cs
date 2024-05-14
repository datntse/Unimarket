using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;
using Unimarket.Infracstruture.Data;

namespace Unimarket.Infracstruture.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification, Guid>
    {
    }
    public class NotificationRepository : BaseRepository<Notification, Guid>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
