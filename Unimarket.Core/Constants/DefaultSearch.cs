using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Constants
{
    public class DefaultSearch
    {
        public int perPage { get; set; } = 10;
        public int currentPage { get; set; } = 0;
        public String? sortBy { get; set; }
        public bool isAscending { get; set; } = true;
    }
}
