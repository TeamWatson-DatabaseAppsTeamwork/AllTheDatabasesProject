﻿namespace ProductsSystem.Client
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine;
    using ProductsSystem.Engine.UserInterface;

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
            //Configuration.InitializeDatabase(context);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var data = ProductsSystemData.GetInstance(context);
            //var userInterface = new ConsoleUserInterface();
            //var engine = Engine.GetInstance(userInterface, data);
            //engine.Run();


            var product = data.Products.All().Select(
                p => new { ProductName = p.Name, VendorName = p.Vendor.Name, QuantitySold = p.Sales.Sum(s => s.Quantity * p.Price) });


            //Product product = new Product();
            //product.ExpiryDate = new DateTime(2008, 12, 28);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, product);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }


            var json = JsonExporter.JsonExporter.ToJson(product);
            Console.WriteLine(json);
        }
    }
}