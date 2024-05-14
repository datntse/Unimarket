using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Enums;

namespace Unimarket.Core.Models
{
    public class NotiDTO
    {
        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public NotiStatus Status { get; set; }

    }
}
