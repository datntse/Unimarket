using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class UserPackage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public int Quantity{ get; set; }
        public DateTime RegisterAt { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
