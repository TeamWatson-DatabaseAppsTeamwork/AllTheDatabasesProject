namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using OracleImporterForSqlServer;
    using ProductsSystem.Data;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    class ImportOracleDataCommand : IEngineCommand
    {
        private IOracleImporterForSqlServer oracleImporter;
        private ProductsSystemOracleEntities oracleDbContext;

        public ImportOracleDataCommand(
            ProductsSystemOracleEntities oracleDbContext, IOracleImporterForSqlServer oracleImporter)
        {
            this.oracleDbContext = oracleDbContext;
            this.oracleImporter = oracleImporter;
        }

        public IList Arguments { get; private set; }

        public string Execute(IProductsSystemData data)
        {
            var products = this.oracleDbContext.PRODUCTS.
                Select
                (
                    p => new Product
                    {
                        Name = p.NAME,
                        Vendor = new Vendor{ Name = p.VENDORS.NAME },
                        Measure = new Measure { Name = p.MEASURES.NAME }
                    }
                )
                .ToList();
            this.ImportOracleData(data, products);
            return EngineConstants.OracleDataSuccessfullyImported;
        }

        public void RecieveArguments(string[] rawArguments)
        {
            this.Arguments = rawArguments;
        }

        private void ImportOracleData(IProductsSystemData data, List<Product> oracleProdutcs)
        {
            var sqlSereverProductsNames = data.Products.All().Select(p => p.Name);
            oracleProdutcs.RemoveAll(p => sqlSereverProductsNames.Contains(p.Name));
            if (oracleProdutcs.Any())
            {
                data.Products.AddRange(oracleProdutcs);
                data.SaveChanges();
            }
        }
    }
}
