using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class UserNotification
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserIdFor { get; set; }
        public Guid ItemId { get; set; }
        public Guid PostId { get; set; }
        public Guid NotificationId { get; set; }    
        public virtual Post Post { get; set; }
        public virtual Item Item { get; set; }
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
