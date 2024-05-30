using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using catalog.Models;
using catalog.Validations;

namespace catalog.DTO
{
    public class ProductDTO
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
        public decimal Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}