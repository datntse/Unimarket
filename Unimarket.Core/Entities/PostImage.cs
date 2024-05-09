using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimarket.Core.Entities
{
    public class PostImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PostId { get; set; }    
        public String ImageUrl { get; set; }
        public virtual Post Post { get; set; }  
    }
}
