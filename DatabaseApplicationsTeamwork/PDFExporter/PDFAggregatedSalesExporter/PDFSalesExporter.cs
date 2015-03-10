namespace PDFExporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using PDFExporter.PDFAggregatedSalesExporter;

    public class PDFSalesExporter : IPDFExporter
    {
        private const string Heading = "Aggregated Sales Report";
        private const string DefaultFileName = "Aggregated-Sales-Report-From{0}-to{1}.pdf";
        private const int DefaultColumnsNumber = 5;
        private const string DefaultTotalRowHeading = "Total sum for {0}:";
        private const string DefaultDateFormat = "dd-MMM-yyyy";
        private readonly float[] TableDefaultColumnWidths = new[] {2f, 1f, 1f, 3f, 1f};
        private readonly string DefaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly Font DefaultHeadingFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        private readonly Font DefaultAggregatedRowFont = new Font(Font.FontFamily.HELVETICA, 10);
        private readonly Font DefaultCellFont = new Font(Font.FontFamily.HELVETICA, 9);
        private readonly string[] DefaultCellsHeadings = new[] {"Product", "Quantity", "Unit Price", "Location", "Sum"};

        private IList<SalesForDate> data;

        public PDFSalesExporter()
        {
            this.FileFolderPath = DefaultFileFolderPath;
            this.FileName = DefaultFileName;
            this.ColumnsNumber = DefaultColumnsNumber;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public int ColumnsNumber { get; set; }

        public IList<SalesForDate> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public void Export()
        {
            Console.WriteLine(Element.ALIGN_RIGHT);
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
            var tableHeading = this.CreateCell(DefaultHeadingFont, DefaultColumnsNumber, 1, Heading);
            dataTable.AddCell(tableHeading);
            dataTable.SetWidths(TableDefaultColumnWidths);

            foreach (var date in this.Data)
            {
                var dateCell = this.CreateCell(
                    DefaultAggregatedRowFont, DefaultColumnsNumber, 0, date.Date.ToString(DefaultDateFormat));
                dateCell.BackgroundColor = new BaseColor(242, 242, 242);
                dataTable.AddCell(dateCell);
                var cellsHeadings = this.CreateTableRow(DefaultCellsHeadings, 1, new BaseColor(217, 217, 217));

                dataTable.Rows.Add(cellsHeadings);

                foreach (var sale in date.Sales)
                {
                    var row = this.CreateTableRow(sale, 1);
                    dataTable.Rows.Add(row);
                }

                this.CreateTotalRow(date.Date, date.TotaSum, ref dataTable);
            }

            pdfDocument.Add(dataTable);
        }

        private PdfPCell CreateCell(Font font, int colspan, int alignment, string text)
        {
            var cell = new PdfPCell();
            cell.Colspan = colspan;
            var paragraph = new Paragraph(text, font);
            paragraph.Alignment = alignment;
            cell.AddElement(paragraph);

            return cell;
        }

        private PdfPCell CreateCell(Font font, int colspan, BaseColor backgroundColor, int alignment, string text)
        {
            var cell = this.CreateCell(font, colspan, alignment, text);
            cell.BackgroundColor = backgroundColor;
            return cell;
        }

        private PdfPRow CreateTableRow(object rowData, int cellsAlignment)
        {
            var cells = new List<PdfPCell>();

            foreach (var property in rowData.GetType().GetProperties())
            {
                var cell = this.CreateCell(DefaultCellFont, 1, cellsAlignment, property.GetValue(rowData, null).ToString());
                cells.Add(cell);
            }

            var row = new PdfPRow(cells.ToArray());
            return row;
        }

        private PdfPRow CreateTableRow(object rowData, int cellsAlignment, BaseColor backgroundColor)
        {
            var row = this.CreateTableRow(rowData, cellsAlignment);
            foreach (var cell in row.GetCells())
            {
                cell.BackgroundColor = backgroundColor;
            }

            return row;
        }

        private PdfPRow CreateTableRow(object[] rowData, int cellsAlignment)
        {
            var cells = new List<PdfPCell>();

            foreach (var cellData in rowData)
            {
                var cell = this.CreateCell(DefaultCellFont, 1, cellsAlignment, cellData.ToString());
                cells.Add(cell);
            }

            var row = new PdfPRow(cells.ToArray());
            return row;
        }

        private PdfPRow CreateTableRow(object[] rowData, int cellsAlignment, BaseColor backgroundColor)
        {
            var row = this.CreateTableRow(rowData, cellsAlignment);
            foreach (var cell in row.GetCells())
            {
                cell.BackgroundColor = backgroundColor;
            }

            return row;
        }

        private void CreateTotalRow(DateTime date, decimal totalSum, ref PdfPTable dataTable)
        {
            var totalRowHeading = this.CreateCell(
                DefaultAggregatedRowFont, 4, 2,
                    string.Format(DefaultTotalRowHeading, date.ToString(DefaultDateFormat)));
            var totalRowValue = this.CreateCell(
                DefaultAggregatedRowFont, 1, 2,
                    totalSum.ToString());
            dataTable.AddCell(totalRowHeading);
            dataTable.AddCell(totalRowValue);
        }
    }
}
