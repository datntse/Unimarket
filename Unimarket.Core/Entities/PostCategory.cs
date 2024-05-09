using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class PostCategory
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PostId { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Category>? Categories{ get; set; }
    }
}
