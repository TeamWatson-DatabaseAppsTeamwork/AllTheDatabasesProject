namespace ProductsSystem.Data.Contexts
{
    using System.Data.Entity;
    using ProductsSystem.Models;

    public class ProductsSystemDbContext : DbContext, IProductsSystemDbContext
    {
        public ProductsSystemDbContext()
            : base("ProductsSystem")
        { 
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Vendor> Vendors { get; set; }

        public IDbSet<Measure> Measures { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
