using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Models
{
    public class CheckOutViewModel
    {
        [Required(ErrorMessage ="Please enter a valid credit card number")]
        [CreditCard (ErrorMessage ="Invalid credit car number") ]
        public String CreditCardNum { get; set; }
    }
}
