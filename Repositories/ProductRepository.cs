using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;

namespace catalog.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetProductsForCategory(int id)
        {
            return GetAll().Where(c => c.CategoryId == id);
        }
    }
}