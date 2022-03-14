using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validators
{
    public class MininmumAllowedAttributes: ValidationAttribute
    {

        public class MinimimAllowedYearAttribute : ValidationAttribute
        {
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                // get the user entered value
                var userEnetredYear = ((DateTime)value).Year;

                if (userEnetredYear < 1900)
                {
                    return new ValidationResult("Year should be no less than 1900");
                }
                return ValidationResult.Success;
            }
        }

    }
}
