namespace ProductsSystem.Data.Data
{
    using ProductsSystem.Data.Repositories;
    using ProductsSystem.Models;

    public interface IProductsSystemData
    {
        IProductsRepository Products { get; }

        IRepository<Vendor> Vendors { get; }

        IRepository<Measure> Measures { get; }

        int SaveChanges();
    }
}
