namespace PDFExporter
{
    using ProductsSystem.Data;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class EntryPoint
    {
        public static void Main()
        {
            var context = new ProductsSystemDbContext();

            // Execute the following method if do not have the
            // database in sql server
            // It will add sample data automatically
            // Configuration.InitializeDatabase(context);

            var data = ProductsSystemData.GetInstance(context);

            var product = new Product
            {
                Name = "Chocolate \"Milka\"",
                Price = (decimal)2.80,
                VendorId = 1,
                MeasureId = 2
            };

            data.Products.Add(product);

            data.SaveChanges();
        }
    }
}
