namespace OracleImporterForSqlServer
{
    using ProductsSystem.Data.Data;

    public interface IOracleImporterForSqlServer
    {
        void Import(IProductsSystemData data);
    }
}
