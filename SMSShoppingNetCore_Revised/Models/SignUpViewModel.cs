using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Models
{
    public class SignUpViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Key]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage ="Password Confirmation needs to match Password")]
        public string PasswordConfirmation { get; set; }
    }
}
