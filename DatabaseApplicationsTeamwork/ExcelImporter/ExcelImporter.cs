namespace ExcelImporter
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class ExcelImporter : IExcelImporter
    {
        public IList<Sale> DataToBeImported { get; set; }

        public void Import(IProductsSystemData data)
        {
            data.Sales.AddRange(this.DataToBeImported);
            data.SaveChanges();
        }
    }
}
