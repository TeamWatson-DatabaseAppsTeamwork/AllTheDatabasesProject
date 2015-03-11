namespace ProductsSystem.Data.Contexts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ProductsSystem.Models;

    public interface IProductsSystemDbContext
    {
        IDbSet<Product> Products { get; set; }

        IDbSet<Vendor> Vendors { get; set; }

        IDbSet<Measure> Measures { get; set; }

        IDbSet<Supermarket> Supermarkets { get; }

        IDbSet<Sale> Sales { get; } 

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
