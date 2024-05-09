using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace catalog.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            var categoryWithProducts = _context.Categories.Include(c => c.Products).ToList();
            return categoryWithProducts;
        }
    }
}