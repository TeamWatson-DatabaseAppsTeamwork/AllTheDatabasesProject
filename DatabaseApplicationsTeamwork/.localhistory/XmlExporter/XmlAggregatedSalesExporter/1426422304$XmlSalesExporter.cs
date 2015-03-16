namespace XmlExporter.XmlAggregatedSalesExporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using ProductsSystem.DataTransferObjects;

    public class XmlSalesExporter : IXmlExporter
    {
        // File Parameters
        private const string DefaultFileName = "Sales-by-Vendors-Report.xml";
        private readonly string defaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private IList<SalesAggregated> data;

        //public XmlSalesExporter(IList<SalesAggregated> aggregatedSalesData)
        //{
        //    this.FileFolderPath = this.defaultFileFolderPath;
        //    this.FileName = DefaultFileName;
        //    this.Data = aggregatedSalesData;
        //}

        public XmlSalesExporter()
        {
            this.FileFolderPath = this.defaultFileFolderPath;
            this.FileName = DefaultFileName;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public IList<SalesAggregated> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public string DefaultFileFolderPath { get; set; }

        public IList Arguments { get; private set; }

        public void Export(IList<SalesAggregated> salesAggregated)
        {
            var filePath = this.FileFolderPath + "\\" + this.FileName;

            //var context = new ProductsSystemDbContext();

            //Repository<Sale> sales = new Repository<ProductsSystem.Models.Sale>(context);
            //var salesAggregated = sales.All()
            //    .GroupBy(s => s.Product.Vendor)
            //    .Select(sgv => new SalesAggregated
            //    {
            //        VendorName = sgv.Key.Name,
            //        RawSummaries = sgv.GroupBy(s => s.Date).Select(sgd => new SalesSummary { Date = sgd.Key, TotalSum = sgd.Sum(s => s.Product.Price * s.Quantity) })
            //    }).ToList();

            var serializer = new XmlSerializer(typeof(List<SalesAggregated>));
            TextWriter textWriter = new StreamWriter(filePath); //changed filepath
            serializer.Serialize(textWriter, salesAggregated);
            textWriter.Close();

            Console.WriteLine("work done - check temp folder on c: ;)");
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
