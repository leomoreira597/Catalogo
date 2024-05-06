using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.Validations
{
    public class FirstCapsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
            var firstString = value.ToString()![0].ToString();
            if (firstString != firstString.ToUpper())
            {
                return new ValidationResult("A primeira letra deve ser maiuscula");
            }
            return ValidationResult.Success;
        }
    }
}