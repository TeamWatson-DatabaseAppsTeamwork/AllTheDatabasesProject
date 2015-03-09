namespace ProductsSystem.Data
{
    using System.Data.Entity;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Models;

    public class ProductsSystemDbContext : DbContext, IProductsSystemDbContext
    {
        public ProductsSystemDbContext() : base("ProductsSystem") { }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Measure> Measures { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
