using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class Package
    {
        [Key]
        public Guid Id { get; set; }    
        public String Name { get; set; }
        public String Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
