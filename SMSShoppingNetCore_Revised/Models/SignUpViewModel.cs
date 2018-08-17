using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SMSShoppingNetCore_Revised.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Please enter first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please enter email address.")]
        [EmailAddress]
        [Key]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [MinLength(6, ErrorMessage ="Password needs to be at least 6 characters.")]
        [NeedsUpperCase]
        [NeedsNumeric]
        [NeedsNonAlphaNumeric]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please confirm password.")]
        [Compare("Password", ErrorMessage = "Confirmation password needs to match password.")]
        public string PasswordConfirmation { get; set; }
    }

    public class NeedsUpperCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SignUpViewModel model = (SignUpViewModel)validationContext.ObjectInstance;
            if(model.Password != null)
            {
                if (!model.Password.Any(char.IsUpper))
                {
                    return new ValidationResult("Password needs to contain an uppercase letter.");
                }
            }
            
            return ValidationResult.Success;
        }
    }

    public class NeedsNonAlphaNumericAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SignUpViewModel model = (SignUpViewModel)validationContext.ObjectInstance;

            if (model.Password != null)
            {
                if (!model.Password.Any(char.IsSymbol))
                {
                    return new ValidationResult("Password needs to contain a symbol.");
                }
            }
            
            return ValidationResult.Success;
        }
    }
    public class NeedsNumericAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SignUpViewModel model = (SignUpViewModel)validationContext.ObjectInstance;

            if (model.Password != null)
            {
                if (!model.Password.Any(char.IsDigit))
                {
                    return new ValidationResult("Password needs to contain a number.");
                }
            }
            
            return ValidationResult.Success;
        }
    }
}
