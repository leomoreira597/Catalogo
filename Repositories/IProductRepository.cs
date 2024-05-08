using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Models;

namespace catalog.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();
        Product GetProduct(int id);
        Product Create(Product product);
        bool Update(Product product);
        bool Delete(int id);
    }
}