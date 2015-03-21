namespace OracleImporterForSqlServer
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public interface IOracleImporterForSqlServer
    {
        List<Product> DataToBeImported { get; set; }

        void Import(IProductsSystemData data);
    }
}
