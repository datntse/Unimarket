using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Models
{
    public class UserNotiDTO
    {
        [Required(ErrorMessage = "userID is required")]
        public string userId { get; set; }
        [Required(ErrorMessage = "Notication is required")]
        public Guid NotificationId { get; set; }
        public Guid ItemId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserIdFor { get; set; }
    }
}
