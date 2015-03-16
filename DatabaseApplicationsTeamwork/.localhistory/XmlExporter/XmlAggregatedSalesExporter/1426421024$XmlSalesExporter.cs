namespace XmlExporter.XmlAggregatedSalesExporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using PdfExporter.PdfAggregatedSalesExporter;

    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Repositories;
    using ProductsSystem.DataTransferObjects;
    using ProductsSystem.Models;

    public class XmlSalesExporter : IXmlExporter
    {
        private IList<SalesForDateInterval> data;

        public XmlSalesExporter()
        {
            this.FileFolderPath = this.DefaultFileFolderPath;
            this.FileName = this.DefaultFileName;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public IList<SalesForDateInterval> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public string DefaultFileFolderPath { get; set; }

        public string DefaultFileName { get; set; }

        public IList Arguments { get; private set; }

        public void Export()
        {
            //var filePath = this.FileFolderPath + "\\" + this.FileName;
            //var pdfDocument = new Document(PageSize.A4);
            //var output = new FileStream(filePath, FileMode.Create);
            //var pdfWriter = PdfWriter.GetInstance(pdfDocument, output);

            //using (pdfWriter)
            //{
            //    pdfDocument.Open();
            //    this.PopulateDataTable(ref pdfDocument);
            //    pdfDocument.Close();
            //}

            var context = new ProductsSystemDbContext();

            var sales = new Repository<ProductsSystem.Models.Sale>(context);
            var salesAggregated = sales.All()
                .GroupBy(s => s.Product.Vendor)
                .Select(sgv => new SalesAggregated
                {
                    VendorName = sgv.Key.Name,
                    RawSummaries = sgv.GroupBy(s => s.Date).Select(sgd => new SalesSummary { Date = sgd.Key, TotalSum = sgd.Sum(s => s.Product.Price * s.Quantity) })
                }).ToList();

            var serializer = new XmlSerializer(typeof(List<SalesAggregated>));
            TextWriter textWriter = new StreamWriter(@"c:\temp\Sales-by-Vendors-Report.xml");
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
