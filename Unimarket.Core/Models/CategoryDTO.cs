using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Models
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }

    public class CategoryUM
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }
}
