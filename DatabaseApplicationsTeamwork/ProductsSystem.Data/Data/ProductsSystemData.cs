namespace ProductsSystem.Data.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Repositories;
    using ProductsSystem.Models;

    public class ProductsSystemData : IProductsSystemData
    {
        private IProductsSystemDbContext context;
        private IDictionary<Type, object> repositories;
        private static ProductsSystemData dataInstance;

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

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public static ProductsSystemData GetInstance(IProductsSystemDbContext context)
        {
            if (dataInstance == null)
            {
                dataInstance = new ProductsSystemData(context);
            }

            return dataInstance;
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof (T);
            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof (Repository<T>);
                if (!typeof(T).IsAssignableFrom(repositoryType))
                {
                    repositoryType = typeof (ProductsRepository);
                }

                this.repositories.Add(type, Activator.CreateInstance(repositoryType, this.context));
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
