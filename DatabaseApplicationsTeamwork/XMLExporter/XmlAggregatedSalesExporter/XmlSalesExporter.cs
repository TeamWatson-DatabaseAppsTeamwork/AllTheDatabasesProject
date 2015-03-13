namespace XmlExporter.XmlAggregatedSalesExporter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using PdfExporter.PdfAggregatedSalesExporter;

    using ProductsSystem.Data.Contexts;
    using ProductsSystem.Data.Repositories;
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
            var vendors = new Repository<Vendor>(context);
            var count = 1;
            foreach (var vendor in vendors.All())
            {
                //var tempVendor = new Vendor()
                //                      {
                //                          Name = vendor.Name
                //                      };
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Vendor));

                var file = new System.IO.StreamWriter(
                    @"c:\temp\TestXmlExport" + count + ".xml");
                writer.Serialize(file, vendor);
                file.Close();
                Console.WriteLine("work done");
                count++;
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
