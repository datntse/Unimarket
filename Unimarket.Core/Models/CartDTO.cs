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
        public string userId { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int Quantity { get; set; }
        public ItemCartVM Item { get; set; }
        
    }
    public class ItemCartVM
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public float Price { get; set; }
        public String ImageUrl { get; set; }
    }

    public class AddItemDTO
    {
        public String UserId { get; set; }
        public String ItemId { get; set; }
    }
    public class UpdateItemQuantityDTO
    {
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
