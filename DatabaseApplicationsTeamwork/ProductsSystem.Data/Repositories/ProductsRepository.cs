using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsSystem.Data.Repositories
{
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Models;

    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(IProductsSystemDbContext context)
            : base(context)
        {
        }
    }
}
