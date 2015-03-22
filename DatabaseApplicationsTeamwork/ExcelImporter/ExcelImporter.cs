namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class ExcelImporter : IExcelImporter
    {
        public IList<Sale> SalesToBeImported { get; set; }

        public void Import(IProductsSystemData data)
        {
            data.Sales.AddRange(this.SalesToBeImported);
            data.SaveChanges();
        }
    }
}
