namespace XmlImporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class XmlExpensesImporter : IXmlImporter
    {
        public IDictionary<string, IList<KeyValuePair<DateTime, decimal>>>
            DataToBeImported { get; set; }

        public void Import(ProductsSystem.Data.Data.IProductsSystemData data)
        {
            
        }
    }
}
