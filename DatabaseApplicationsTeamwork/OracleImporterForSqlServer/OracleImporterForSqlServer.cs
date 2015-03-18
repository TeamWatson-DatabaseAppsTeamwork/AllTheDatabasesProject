namespace OracleImporterForSqlServer
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class OracleImporterForSqlServer : IOracleImporterForSqlServer
    {
        public IList<Product> DataToBeImported { get; set; }

        public void Import(IProductsSystemData data)
        {
            // TODO detect duplicated products
        }
    }
}
