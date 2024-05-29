using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Name { get; set; }
        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}