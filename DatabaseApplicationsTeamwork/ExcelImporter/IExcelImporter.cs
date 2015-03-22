namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public interface IExcelImporter
    {
        IList<Sale> SalesToBeImported { get; set; }
 
        void Import(IProductsSystemData data);
    }
}
