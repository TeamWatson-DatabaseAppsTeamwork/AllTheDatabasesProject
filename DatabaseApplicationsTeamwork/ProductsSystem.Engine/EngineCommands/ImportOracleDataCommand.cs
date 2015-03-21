namespace ProductsSystem.Engine.EngineCommands
{
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
            var dataToBeImported = this.LoadOracleData();
            oracleImporter.DataToBeImported = dataToBeImported;
            oracleImporter.Import(data);
            return EngineConstants.OracleDataSuccessfullyImported;
        }

        public void RecieveArguments(string[] rawArguments)
        {
            this.Arguments = rawArguments;
        }

        private List<Product> LoadOracleData()
        {
            var oracleProducts = this.oracleDbContext.PRODUCTS.
               Select
               (
                   p => new Product
                   {
                       Name = p.NAME,
                       VendorId = (int)p.VENDORS.ID,
                       MeasureId = (int)p.MEASURES.ID
                   }
               )
               .ToList();

            return oracleProducts;
        }
    }
}
