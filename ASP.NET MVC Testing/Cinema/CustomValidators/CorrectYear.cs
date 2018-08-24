using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.CustomValidators
{

    public class CorrectYear : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " is required.");
            }
            int year = (int)value;
            if (year >= 1895 && year <= System.DateTime.Now.Year)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Wrong film release date.");
        }
    }
}