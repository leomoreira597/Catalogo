using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.DTO
{
    public class ProductDtoUpdateResponse
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}