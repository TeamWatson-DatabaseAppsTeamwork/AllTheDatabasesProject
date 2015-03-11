namespace ProductsSystem.Data.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ProductsSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        public static void InitializeDatabase(DbContext context)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductsSystemDbContext, Configuration>());
            context.Database.Initialize(true);
        }

        protected override void Seed(ProductsSystemDbContext context)
        {
            this.AddVendors(context);
            this.AddMeasures(context);
            this.AddProducts(context);
            this.AddSupermarkets(context);
        }

        private void AddVendors(ProductsSystemDbContext db)
        {
            db.Vendors.AddOrUpdate(
                new Vendor { Name = "Nestle Sofia Corp." },
                new Vendor { Name = "Zagorka Corp." },
                new Vendor { Name = "Targovishte Bottling Company Ltd." },
                new Vendor { Name = "Ivena Comers-GD" },
                new Vendor { Name = "Daris Honey Trading" },
                new Vendor { Name = "Max Medica" },
                new Vendor { Name = "Frut Corect" },
                new Vendor { Name = "Vantea" },
                new Vendor { Name = "Sams97" },
                new Vendor { Name = "RelexEp" },
                new Vendor { Name = "Avis" },
                new Vendor { Name = "AquaTrade" },
                new Vendor { Name = "Elixir" },
                new Vendor { Name = "E-bakalia" },
                new Vendor { Name = "Radex" },
                new Vendor { Name = "Dovex" },
                new Vendor { Name = "VSDrinks" },
                new Vendor { Name = "Toby Ltd." },
                new Vendor { Name = "Brevis" },
                new Vendor { Name = "Seva Ltd." });

            ((DbContext)db).SaveChanges();
        }

        private void AddMeasures(ProductsSystemDbContext db)
        {
            db.Measures.AddOrUpdate(
                new Measure { Name = "liter" },
                new Measure { Name = "gram" },
                new Measure { Name = "kilogram" },
                new Measure { Name = "milliliter" });

            ((DbContext)db).SaveChanges();
        }

        private void AddProducts(ProductsSystemDbContext db)
        {
            db.Products.AddOrUpdate(
                new Product
                {
                    Name = "Beer “Zagorka",
                    Price = (decimal)0.86,
                    VendorId = 2,
                    MeasureId = 1
                },
                new Product
                {
                    Name = "Vodka “Targovishte",
                    Price = (decimal)7.56,
                    VendorId = 3,
                    MeasureId = 1
                },
                new Product
                {
                    Name = "Beer \"Beck’s",
                    Price = (decimal)1.03,
                    VendorId = 2,
                    MeasureId = 1
                },
                new Product
                {
                    Name = "Chocolate \"Milka\"",
                    Price = (decimal)2.80,
                    VendorId = 1,
                    MeasureId = 2
                });

            ((DbContext)db).SaveChanges();
        }

        private void AddSupermarkets(ProductsSystemDbContext db)
        {
            db.Supermarkets.AddOrUpdate
            (
              new Supermarket
              {
                  Location = ""
              }  
            );

            ((DbContext)db).SaveChanges();
        }
    }
}
