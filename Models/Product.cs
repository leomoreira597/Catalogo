using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        public float Stock { get; set; }
        public DateTime DateRegister { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}