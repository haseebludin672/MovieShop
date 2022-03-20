using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validators
{
    public class MinimumAllowedYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the user entered value
            var userEnteredYear = ((DateTime)value).Year;

            if (userEnteredYear < 1900)
            {
                return new ValidationResult("Year should be no less than 1900");
            }

            return ValidationResult.Success;
        }
    }
}
