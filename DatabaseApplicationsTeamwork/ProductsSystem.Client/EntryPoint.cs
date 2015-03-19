namespace ProductsSystem.Client
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Data.Migrations;
    using ProductsSystem.DataTransferObjects;
    using ProductsSystem.Engine;
    using ProductsSystem.Engine.UserInterface;

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new ProductsSystemDbContext();

            // Test retrive data from mysql
            var db = new ProductsSystemDbContextForMySql();
            var measures = db.Measures.ToList().First();
            Console.WriteLine(measures.Name);

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

            //var product = data.Sales.All()
            //    .GroupBy(s => s.Product)
            //    .Select(sp => new SalesByProduct
            //    {
            //        ProductId = sp.Key.Id,
            //        ProductName = sp.Key.Name,
            //        VendorName = sp.Key.Vendor.Name,
            //        TotalQuantitySold = sp.Sum(s => s.Quantity),
            //        TotalIncomes = sp.Sum(s => s.Quantity * s.Product.Price),
            //    }).ToList();


            //Product product = new Product();
            //product.ExpiryDate = new DateTime(2008, 12, 28);

            //var defaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //var filePath = defaultFileFolderPath + "\\json.txt";

            //var serializer = new JsonSerializer();
            //serializer.Converters.Add(new JavaScriptDateTimeConverter());
            //serializer.NullValueHandling = NullValueHandling.Ignore;

            //using (var sw = new StreamWriter(filePath))
            //using (JsonWriter writer = new JsonTextWriter(sw))
            //{
            //    serializer.Serialize(writer, product);
            //    // {"ExpiryDate":new Date(1230375600000),"Price":0}
            //}


            //var json = JsonExporter.JsonExporter.ToJson(product);
            //Console.WriteLine(json);
        }
    }
}
