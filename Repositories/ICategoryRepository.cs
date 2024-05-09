using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using catalog.Models;

namespace catalog.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
         IEnumerable<Category> GetCategoriesWithProducts();
    }
}