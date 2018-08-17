using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage ="Email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}
