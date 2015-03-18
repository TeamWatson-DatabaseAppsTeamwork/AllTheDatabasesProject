namespace TestMySqlConnection
{
    using System;
    using System.Linq;
    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Migrations;

    class Program
    {
        static void Main(string[] args)
        {
            var db = new ProductsSystemDbContext("MySql");
            var measures = db.Measures.ToList().First();
            Console.WriteLine(measures.Name);
        }
    }
}
