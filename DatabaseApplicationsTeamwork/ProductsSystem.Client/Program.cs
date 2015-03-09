namespace ProductsSystem.Client
{
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Data;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new ProductsSystemDbContext();

            // Execute the following method if do not have the
            // database in sql server
            // It will add sample data automatically
            // Configuration.InitializeDatabase(context);
            var data = ProductsSystemData.GetInstance(context);
        }
    }
}
