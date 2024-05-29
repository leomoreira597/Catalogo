using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.Repositories
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Commit();
    }
}