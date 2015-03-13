namespace XmlExporter.XmlAggregatedSalesExporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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

            var sales = new Repository<Sale>(context);
            var salesAggregated = sales.All()
                .GroupBy(s => new { s.Product.Vendor, s.Date })
                .Select(sg => new SalesAggregated
                {
                    VendorName = sg.Key.Vendor.Name,
                    Date = sg.Key.Date,
                    TotalSum = sg.Sum(s => s.Quantity * s.Product.Price),
                })
                .ToList();

            var count = 1;
            foreach (var sale in salesAggregated)
            {
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(SalesAggregated));
                var file = new System.IO.StreamWriter(
                    @"c:\temp\TestXmlExport" + count + ".xml");
                writer.Serialize(file, sale);
                file.Close();
                count++;
            }

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
