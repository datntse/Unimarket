using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Models
{
    public class PaymentRequestDTO
    {
        public Guid PackageId { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string OrderDescription { get; set; }
        public string BankCode { get; set; }
        public string Locale { get; set; }
    }
}
