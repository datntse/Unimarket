using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;

namespace Unimarket.Core.Models
{
    public class OrderDTO
    {
        [Key]
        public Guid Id { get; set; }
        public String PaymentType { get; set; }
        public float TotalPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }

    public class CheckOutDTO 
    {
        public string UserId {  get; set; }
        public string PaymentType { get; set; }
    }
}
