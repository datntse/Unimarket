using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Models
{
    public class ItemDTO
    {
        public Guid Id { get; set; } 
        public String Name { get; set; }
        public String ProductDetail { get; set; }
        public String Description { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public String ImageUrl { get; set; }
        public List<String> CategoryId { get; set; }  
        public List<String> SubImageUrl { get; set; }
    }

    public class ItemVM
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ProductDetail { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public String ImageUrl { get; set; }
        public List<String> CategoryName { get; set; }
        public List<String> SubImageUrl { get; set; }
    }
}
