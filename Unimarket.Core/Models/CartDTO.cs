using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;

namespace Unimarket.Core.Models
{
    public class CartDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }

    public class AddItemDTO
    {
        public Guid ItemId { get; set; }
        public string UserId {  get; set; }
    }
    public class UpdateItemQuantityDTO
    {
        public Guid ItemId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
