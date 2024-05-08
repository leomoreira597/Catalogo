using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Context;
using catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace catalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category Create(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetCategories()
        {
           return _context.Categories.AsNoTracking().ToList();
        }

        public IEnumerable<Category> GetCategoriesWithProducts()
        {
             return _context.Categories.Include(product => product.Products).AsNoTracking().ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(category => category.CategoryId == id);
        }

        public Category Update(Category category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return category;
        }
    }
}