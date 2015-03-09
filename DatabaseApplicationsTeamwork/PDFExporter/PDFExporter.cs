namespace PDFExporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PDFExporter : IPDFExporter
    {
        private const string DefaultFileName = "Aggregated-Sales-Report-From{0}-to{1}.pdf";
        private readonly string DefaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private IDictionary<DateTime, IList<object>> data;
        
        public PDFExporter()
        {
            this.FileFolderPath = DefaultFileFolderPath;
            this.FileName = DefaultFileName;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public IDictionary<DateTime, IList<object>> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public void Export()
        {
            var filePath = this.FileFolderPath + "\\" + String.Format(this.FileName, "", "");

            var pdfDocument = new Document(PageSize.A4);
            var output = new FileStream(filePath, FileMode.Create);
            var pdfWriter = PdfWriter.GetInstance(pdfDocument, output);

            pdfDocument.Open();

            pdfDocument = this.PopulateDataTable(pdfDocument);

            pdfDocument.Close();
        }

        public void SetDefaultFileFolder(string fileFolderPath)
        {
            this.FileFolderPath = fileFolderPath;
        }

        public void SetFileName(string fileName)
        {
            this.FileName = fileName;
        }

        // Test implementation of the method
        private Document PopulateDataTable(Document pdfDocument)
        {
            var dataTable = new PdfPTable(5);
            dataTable.DefaultCell.Border = 0;
            dataTable.WidthPercentage = 90;


            var font = new Font();

            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 1;
            cell11.AddElement(new Paragraph("ABC Traders Receipt", font));
            cell11.AddElement(new Paragraph("Thankyou for shoping at ABC traders,your order details are below", font));
            cell11.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell12 = new PdfPCell();
            cell12.VerticalAlignment = Element.ALIGN_CENTER;


            dataTable.AddCell(cell11);

            dataTable.AddCell(cell12);


            PdfPTable table2 = new PdfPTable(3);

            //One row added

            PdfPCell cell21 = new PdfPCell();

            cell21.AddElement(new Paragraph("Photo Type"));

            PdfPCell cell22 = new PdfPCell();

            cell22.AddElement(new Paragraph("No. of Copies"));

            PdfPCell cell23 = new PdfPCell();

            cell23.AddElement(new Paragraph("Amount"));


            table2.AddCell(cell21);

            table2.AddCell(cell22);

            table2.AddCell(cell23);


            //New Row Added

            PdfPCell cell31 = new PdfPCell();

            cell31.AddElement(new Paragraph("Safe"));

            cell31.FixedHeight = 300.0f;

            PdfPCell cell32 = new PdfPCell();

            cell32.AddElement(new Paragraph("2"));

            cell32.FixedHeight = 300.0f;

            PdfPCell cell33 = new PdfPCell();

            cell33.AddElement(new Paragraph("20.00 * " + "2" + " = " + (20 * Convert.ToInt32("2")) + ".00"));

            cell33.FixedHeight = 300.0f;



            table2.AddCell(cell31);

            table2.AddCell(cell32);

            table2.AddCell(cell33);


            PdfPCell cell2A = new PdfPCell(table2);

            cell2A.Colspan = 2;

            dataTable.AddCell(cell2A);

            PdfPCell cell41 = new PdfPCell();

            cell41.AddElement(new Paragraph("Name : " + "ABC"));

            cell41.AddElement(new Paragraph("Advance : " + "advance"));

            cell41.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell42 = new PdfPCell();

            cell42.AddElement(new Paragraph("Customer ID : " + "011"));

            cell42.AddElement(new Paragraph("Balance : " + "3993"));

            cell42.VerticalAlignment = Element.ALIGN_RIGHT;


            dataTable.AddCell(cell41);

            dataTable.AddCell(cell42);



            pdfDocument.Add(dataTable);
            return pdfDocument;
        }
    }
}
