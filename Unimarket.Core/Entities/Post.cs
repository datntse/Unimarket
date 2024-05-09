using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class Post
    {
        [Key]
        public Guid Id {  get; set; }
        public String Title { get; set; }   
        public String Description { get; set; }   
        public String Image{ get; set; }   
        public bool IsActive  { get; set; }   
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<PostImage>? PostImages { get; set; }
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }

    }
}
