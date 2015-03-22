namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class ExcelImporter : IExcelImporter
    {
        public IList<Sale> SalesToBeImported { get; set; }

        public IList<Price> PricesToBeImported { get; set; } 

        public void ImportSales(IProductsSystemData data)
        {
            data.Sales.AddRange(this.SalesToBeImported);
            data.SaveChanges();
        }

        public void ImportPrices(IProductsSystemData data)
        {
            data.Prices.AddRange(this.PricesToBeImported);
            data.SaveChanges();
        }
    }
}
