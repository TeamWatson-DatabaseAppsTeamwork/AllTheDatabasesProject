namespace XmlImporter
{
    using System;
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;

    public interface IXmlImporter
    {
        IDictionary<string, IList<KeyValuePair<DateTime, decimal>>>
            DataToBeImported { get; set; }

        void Import(IProductsSystemData data);
    }
}
