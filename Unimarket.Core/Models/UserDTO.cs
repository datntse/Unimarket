using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unimarket.Core.Entities;

namespace Unimarket.Core.Models
{
    public class UserDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        [StringLength(10)]
        public String? DOB { get; set; }
        public String? PhoneNumber { get; set; }
        public String? CCCDNumber { get; set; }
        public bool Gender { get; set; }
        // default Status = 1;
        public int Status { get; set; } = 1;

    }

    public class UserRolesVM : UserDTO
    {
        public String UserName { get; set; }
        public String Id { get; set; }
        public String Email { get; set; }
        public Boolean IsActive { get; set; }
        public List<string> RolesName { get; set; }
    }

    public class UserRoles : ApplicationUser
    {
        public List<string> RolesName { get; set; }
    }

    public class UserSignUp : UserDTO
    {
        public String Password { get; set; }
    }

    public class UserSignIn
    {
        public String UserName { get; set; }
        public String Password { get; set; }
    }

    public class UserSignInVM : UserSignIn
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
    

}
