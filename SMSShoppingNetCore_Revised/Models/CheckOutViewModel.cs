using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Models
{
    public class CheckOutViewModel
    {
        [Required]
        [CreditCard]
        public String CreditCardNum { get; set; }
    }
}
