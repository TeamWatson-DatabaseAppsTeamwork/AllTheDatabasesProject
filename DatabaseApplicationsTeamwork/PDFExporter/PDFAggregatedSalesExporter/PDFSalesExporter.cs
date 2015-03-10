namespace PDFExporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PDFSalesExporter : IPDFExporter
    {
        private const string Heading = "Aggregated Sales Report";
        private const string DefaultFileName = "Aggregated-Sales-Report-From{0}-to{1}.pdf";
        private const int DefaultColumnsNumber = 5;
        private readonly float[] TableDefaultColumnWidths = new[] {2f, 1f, 1f, 3f, 1f};
        private readonly string DefaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly Font DefaultHeadingFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        private readonly Font DefaultAggregatedRowFont = new Font(Font.FontFamily.HELVETICA, 12);
        private readonly Font DefaultCellFont = new Font(Font.FontFamily.HELVETICA, 9);

        private IDictionary<DateTime, IList<object>> data;

        public PDFSalesExporter()
        {
            this.FileFolderPath = DefaultFileFolderPath;
            this.FileName = DefaultFileName;
            this.ColumnsNumber = DefaultColumnsNumber;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public int ColumnsNumber { get; set; }

        public IDictionary<DateTime, IList<object>> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public void Export()
        {
            Console.WriteLine(Element.ALIGN_CENTER);
            var filePath = this.FileFolderPath + "\\" + String.Format(this.FileName, "", "");

            var pdfDocument = new Document(PageSize.A4);
            var output = new FileStream(filePath, FileMode.Create);
            var pdfWriter = PdfWriter.GetInstance(pdfDocument, output);

            using (pdfWriter)
            {
                pdfDocument.Open();
                this.PopulateDataTable(ref pdfDocument);
                pdfDocument.Close();
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

        // Test implementation of the method
        private void PopulateDataTable(ref Document pdfDocument)
        {
            var dataTable = new PdfPTable(this.ColumnsNumber);
            dataTable.DefaultCell.Border = 0;
            dataTable.WidthPercentage = 90;
            var headingCell = this.CreateCell(DefaultHeadingFont, DefaultColumnsNumber, Heading, 1);
            dataTable.AddCell(headingCell);
            dataTable.SetWidths(TableDefaultColumnWidths);

            foreach (var date in this.Data)
            {
                var dateCell = this.CreateCell(DefaultAggregatedRowFont, DefaultColumnsNumber, date.ToString(), 0);
                dataTable.AddCell(dateCell);
                foreach (var dataRow in date.Value)
                {
                    var row = this.CreateTableRow(dataRow);
                    dataTable.Rows.Add(row);
                }
            }

            pdfDocument.Add(dataTable);
        }

        private PdfPCell CreateCell(Font font, int colspan, string text, int alignment)
        {
            var cell = new PdfPCell();
            cell.Colspan = colspan;
            var paragraph = new Paragraph(text, font);
            paragraph.Alignment = alignment;
            cell.AddElement(paragraph);

            return cell;
        }

        private PdfPRow CreateTableRow(object rowData)
        {
            var cells = new List<PdfPCell>();

            foreach (var property in rowData.GetType().GetProperties())
            {
                var cell = this.CreateCell(DefaultCellFont, 1, property.GetValue(rowData, null).ToString(), 0);
                cells.Add(cell);
            }

            var row = new PdfPRow(cells.ToArray());
            return row;
        }
    }
}
