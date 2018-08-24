using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.CustomValidators
{

    public class Capitalize : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }

            string stringvalue = value.ToString();
            if (stringvalue != "" && Char.IsUpper(stringvalue, 0))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Must start with capital letter.");
        }
    }
}
        
