namespace XmlImporter
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class XmlExpensesImporter : IXmlImporter
    {
        public IDictionary<string, IList<KeyValuePair<DateTime, decimal>>>
            DataToBeImported { get; set; }

        public void Import(IProductsSystemData data)
        {
            foreach (var vendor in DataToBeImported)
            {
                var vendorEntity = data.Vendors.Find(v => v.Name == vendor.Key);
                if (vendorEntity != null)
                {
                    int vendorId = vendorEntity.First().Id;
                    foreach (var vendorExpense in vendor.Value)
                    {
                        data.Expenses.Add(new Expense
                        {
                            VendorID = vendorId,
                            Amount = vendorExpense.Value,
                            Period = vendorExpense.Key
                        });
                    } 
                }
            }

            data.SaveChanges();
        }
    }
}
