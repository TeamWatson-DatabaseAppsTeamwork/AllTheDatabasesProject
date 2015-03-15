namespace JsonExporter.JsonAggregatedSalesExporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using ProductsSystem.DataTransferObjects;

    public class JsonSalesExporter : IJsonExporter
    {
        // File Parameters
        private const string DefaultFileName = ".json";
        private readonly string defaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private IList<SalesByProduct> data;

        public JsonSalesExporter()
        {
            this.FileFolderPath = this.defaultFileFolderPath;
            this.FileName = DefaultFileName;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public IList<SalesByProduct> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public string DefaultFileFolderPath { get; set; }

        public IList Arguments { get; private set; }

        public void Export(IList<SalesByProduct> salesAggregated)
        {
            var serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            foreach (var saleByProduct in salesAggregated)
            {
                var filePath = this.FileFolderPath + "\\" + saleByProduct.ProductId + this.FileName;

                using (var sw = new StreamWriter(filePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, saleByProduct);
                }
            } 
        }

        public void SetDefaultFileFolder(string fileFolderPath)
        {
            this.FileFolderPath = fileFolderPath;
        }

        public void SetFileName(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
