using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Models
{
    public class UserNotiDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "userID is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Notication is required")]
        public Guid NotificationId { get; set; }
        public Guid ItemId { get; set; }
        public Guid PostId { get; set; }
        public string? UserIdFor { get; set; }
    }

    public class UserNotiCM
    {
        public string UserId { get; set; }
        public Guid NotificationId { get; set; }
        [DefaultValue(null)]
        public string? ItemId { get; set; }
        [DefaultValue(null)]
        public string? PostId { get; set; }
        [DefaultValue(null)]
        public string? UserIdFor { get; set; }
    }
}
