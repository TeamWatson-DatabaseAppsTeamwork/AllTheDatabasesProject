namespace ProductsSystem.Data.Data
{
    using System;
    using System.Collections.Generic;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Repositories;
    using ProductsSystem.Models;

    public class ProductsSystemData : IProductsSystemData
    {
        private static ProductsSystemData dataInstance;
        private IProductsSystemDbContext context;
        private IDictionary<Type, object> repositories;

        protected ProductsSystemData(IProductsSystemDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IProductsRepository Products
        {
            get { return (IProductsRepository)this.GetRepository<Product>(); }
        }

        public IRepository<Vendor> Vendors
        {
            get { return this.GetRepository<Vendor>(); }
        }

        public IRepository<Measure> Measures
        {
            get { return this.GetRepository<Measure>(); }
        }

        public IRepository<Supermarket> Supermarkets
        {
            get { return this.GetRepository<Supermarket>(); }
        }

        public IRepository<Sale> Sales
        {
            get { return this.GetRepository<Sale>(); }
        }

        public static ProductsSystemData GetInstance(IProductsSystemDbContext context)
        {
            if (dataInstance == null)
            {
                dataInstance = new ProductsSystemData(context);
            }

            return dataInstance;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<T>);
                if (typeof(Product).IsAssignableFrom(type))
                {
                    repositoryType = typeof(ProductsRepository);
                }

                this.repositories.Add(type, Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
