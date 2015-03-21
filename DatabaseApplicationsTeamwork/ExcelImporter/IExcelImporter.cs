namespace ExcelImporter
{
    using ProductsSystem.Data.Data;

    public interface IExcelImporter
    {
        void Import(IProductsSystemData data);

    }
}
