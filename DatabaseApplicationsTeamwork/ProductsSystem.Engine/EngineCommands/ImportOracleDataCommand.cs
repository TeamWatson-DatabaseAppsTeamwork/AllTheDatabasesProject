namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Linq;
    using OracleImporterForSqlServer;
    using ProductsSystem.Data;
    using ProductsSystem.Data.Data;

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
            Console.WriteLine("Oracle data imported into sql server - test");
            var measureOracle = oracleDbContext.MEASURES.ToList().First();
            Console.WriteLine(measureOracle.NAME);
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
            this.Arguments = rawArguments;
        }
    }
}
