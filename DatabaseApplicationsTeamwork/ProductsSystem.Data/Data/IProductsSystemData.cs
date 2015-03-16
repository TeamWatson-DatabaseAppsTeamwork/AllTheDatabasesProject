namespace ProductsSystem.Data.Data
{
    using ProductsSystem.Data.Repositories;
    using ProductsSystem.Models;

    public interface IProductsSystemData
    {
        IProductsRepository Products { get; }

        IRepository<Vendor> Vendors { get; }

        IRepository<Measure> Measures { get; }

        IRepository<Supermarket> Supermarkets { get; }

        IRepository<Sale> Sales { get; }

        IRepository<Expense> Expenses { get; }

        IRepository<Price> Prices { get; } 

        int SaveChanges();
    }
}
