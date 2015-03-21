namespace OracleImporterForSqlServer
{
    using System.Collections.Generic;
    using System.Linq;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class OracleImporterForSqlServer : IOracleImporterForSqlServer
    {
        public List<Product> DataToBeImported { get; set; }

        public void Import(IProductsSystemData data)
        {
            var sqlSereverProductsNames = data.Products.All().Select(p => p.Name);
            this.DataToBeImported.RemoveAll(p => sqlSereverProductsNames.Contains(p.Name));
            if (this.DataToBeImported.Any())
            {
                data.Products.AddRange(this.DataToBeImported);
                data.SaveChanges();
            }
        }
    }
}
