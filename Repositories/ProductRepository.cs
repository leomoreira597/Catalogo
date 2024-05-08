using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;

namespace catalog.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Product Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product is not null){
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.FirstOrDefault(product => product.ProductId == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return _context.Products;
        }

        public bool Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            if (_context.Products.Any(p => p.ProductId == product.ProductId))
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}