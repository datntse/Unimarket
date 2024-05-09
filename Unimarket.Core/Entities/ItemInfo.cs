using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class ItemInfo
    {
        [Key]
        public Guid Id { get; set; }
        public int SoldQuantity { get; set; } = 0;
        public int Rate { get; set; } = 5;
        public int CommentQuantity { get; set; } = 0;
        public int Status { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }  
    }
}
