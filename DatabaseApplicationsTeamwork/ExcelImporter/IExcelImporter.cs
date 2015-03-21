namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public interface IExcelImporter
    {
        IList<Sale> DataToBeImported { get; set; }
 
        void Import(IProductsSystemData data);

    }
}
