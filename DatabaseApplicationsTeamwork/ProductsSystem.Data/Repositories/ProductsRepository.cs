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
