using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public String PaymentType { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
