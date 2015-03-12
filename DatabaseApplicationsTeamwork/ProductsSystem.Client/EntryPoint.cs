namespace ProductsSystem.Client
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Data.Migrations;
    using ProductsSystem.Engine;

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new ProductsSystemDbContext();

            // Execute the following method if do not have the
            // database in sql server
            // It will add sample data automatically
            // Firstly ensure that in the App.config file
            // you have the name of your sql server in the
            // connection string
            // Configuration.InitializeDatabase(context);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var data = ProductsSystemData.GetInstance(context);
            var userInterface = new ConsoleUserInterface();
            var engine = Engine.GetInstance(userInterface, data);
            engine.Run();
            //var product = data.Products.All().Select(
            //    p => new {ProductName = p.Name, VendorName = p.Vendor.Name, QuantitySold = p.Sales.Sum(s => s.Quantity * p.Price)});
            //var json = JsonExporter.JsonExporter.ToJson(product);
            //Console.WriteLine(json);
        }
    }
}
