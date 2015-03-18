namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using OracleImporterForSqlServer;
    using ProductsSystem.Data.Data;

    class ImportOracleDataCommand : IEngineCommand
    {
        private IOracleImporterForSqlServer oracleImporter;

        public IList Arguments
        {
            get { throw new NotImplementedException(); }
            private set { }
        }

        public string Execute(IProductsSystemData data)
        {
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
            this.Arguments = rawArguments;
        }
    }
}
