namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public interface IExcelImporter
    {
        IList<Sale> SalesToBeImported { get; set; }

        IList<Price> PricesToBeImported { get; set; } 
 
        void ImportSales(IProductsSystemData data);

        void ImportPrices(IProductsSystemData data);

    }
}
