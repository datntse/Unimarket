using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class UserWallet
    {
        [Key]
        public Guid Id { get; set; }
        public float Ammount { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
