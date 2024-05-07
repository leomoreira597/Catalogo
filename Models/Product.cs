using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using catalog.Validations;

namespace catalog.Models
{
    public class Product : IValidatableObject
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "O nome é obrigatorio")]
        [StringLength(80)]
        [FirstCaps]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime DateRegister { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                var firsrString = this.Name[0].ToString();
                if (firsrString != firsrString.ToUpper())
                {
                    yield return new
                        ValidationResult("A primeira letra tem que ser maiuscula", new[] { nameof(this.Name) });
                }
            }
            if (this.Stock <= 0)
            {
                yield return new
                    ValidationResult("O estoque deve ser maior que zero", new[] { nameof(this.Stock) });
            }
        }
    }
}