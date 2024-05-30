using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.DTO
{
    public class ProductDTOUpadateRequest : IValidatableObject
    {
        [Range(1, 9999, ErrorMessage = "o estoque deve estar entre 1 e 9999")]
        public float Stock { get; set; }
        public DateTime DateRegister { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateRegister.Date <= DateTime.Now)
            {
                yield return new ValidationResult("A data deve ser maior que a data atual",
                new[] { nameof(this.DateRegister) });
            }
        }
    }
}