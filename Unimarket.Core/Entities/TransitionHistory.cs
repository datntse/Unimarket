using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class TransitionHistory
    {
        [Key]
        public Guid Id { get; set; }
        public Guid WalletId { get; set; }
        public float Amount { get; set; }
        public String Method {  get; set; }
        public String Content { get; set; }
        public DateTime CreateAt { get; set; }
        public UserWallet UserWallet { get; set; }
    }
}
