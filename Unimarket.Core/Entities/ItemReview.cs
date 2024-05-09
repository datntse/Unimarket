using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class ItemReview
    {
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public int Rating { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}
