using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Models;

namespace catalog.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsForCategory(int id);
    }
}