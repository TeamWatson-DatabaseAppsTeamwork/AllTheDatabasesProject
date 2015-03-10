namespace ProductsSystem.Client
{
    using System;
    using System.Globalization;
    using System.Threading;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine;

    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new ProductsSystemDbContext();

            // Execute the following method if do not have the
            // database in sql server
            // It will add sample data automatically
            // Configuration.InitializeDatabase(context);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var data = ProductsSystemData.GetInstance(context);
            var userInterface = new ConsoleUserInterface();
            var engine = Engine.GetInstance(userInterface, data);
            engine.Run();
        }
    }
}
