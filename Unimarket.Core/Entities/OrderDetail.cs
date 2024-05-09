using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId {  get; set; }
        public Guid ItemId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
