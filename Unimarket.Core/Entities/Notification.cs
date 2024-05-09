using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get;set; }
        public String Title { get; set; }   
        public String Description { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsRead { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
