namespace PdfExporter.PdfAggregatedSalesExporter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using PdfExporter;

    public class PdfSalesExporter : IPdfExporter
    {
        // File Parameters
        private const string DefaultFileName = "Aggregated-Sales-Report.pdf";
        private readonly string defaultFileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly string[] defaultCellsHeadings = new[] { "Product", "Quantity", "Unit Price", "Location", "Sum" };

        // Headings
        private const string Heading = "Aggregated Sales Report";
        private const string DefaultTotalRowHeading = "Total sum for {0}:";
        private const string DefaultGrandTotalRowHeading = "Grand total:";

        // Table settings
        private const int DefaultColumnsNumber = 5;
        private const string DefaultDateFormat = "dd-MMM-yyyy";
        private readonly float[] tableDefaultColumnWidths = new[] {2f, 1f, 1f, 3f, 1f};

        // Fonts
        private readonly Font defaultHeadingFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
        private readonly Font defaultAggregatedRowFont = new Font(Font.FontFamily.HELVETICA, 10);
        private readonly Font defaultCellFont = new Font(Font.FontFamily.HELVETICA, 9);

        // Colors
        private readonly BaseColor defaultGrandTotalRowColor = new BaseColor(180, 198, 231);
        private readonly BaseColor defaultDateRowColor = new BaseColor(242, 242, 242);
        private readonly BaseColor defaultColumnsHeadingsColor = new BaseColor(217, 217, 217);

        private IList<SalesForDateInterval> data;

        public PdfSalesExporter()
        {
            this.FileFolderPath = this.defaultFileFolderPath;
            this.FileName = DefaultFileName;
            this.ColumnsNumber = DefaultColumnsNumber;
        }

        public string FileFolderPath { get; protected set; }

        public string FileName { get; protected set; }

        public int ColumnsNumber { get; set; }

        public IList<SalesForDateInterval> Data
        {
            get { return this.data; }
            set { this.data = value; }
        }

        public void Export()
        {
            var filePath = this.FileFolderPath + "\\" + this.FileName;
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

        // TODO improve this method - currently test implementation
        private void PopulateDataTable(ref Document pdfDocument)
        {
            var dataTable = new PdfPTable(this.ColumnsNumber);
            dataTable.DefaultCell.Border = 0;
            dataTable.WidthPercentage = 90;
            dataTable.SetWidths(this.tableDefaultColumnWidths);

            this.AddTableHeading(ref dataTable);
            this.AddTableContent(ref dataTable);
            var grandTotalSum = this.Data.Sum(sale => sale.TotaSum);
            this.AddGrandTotalRow(
                DefaultGrandTotalRowHeading, grandTotalSum, this.defaultGrandTotalRowColor, ref dataTable);

            pdfDocument.Add(dataTable);
        }

        private void AddTableHeading(ref PdfPTable dataTable)
        {
            var tableHeading = this.CreateCell(this.defaultHeadingFont, DefaultColumnsNumber, 1, Heading);
            dataTable.AddCell(tableHeading);
        }

        private void AddTableContent(ref PdfPTable dataTable)
        {
            foreach (var date in this.Data)
            {
                var dateCell = this.CreateCell(
                    this.defaultAggregatedRowFont, DefaultColumnsNumber, 0, date.Date.ToString(DefaultDateFormat));
                dateCell.BackgroundColor = this.defaultDateRowColor;
                dataTable.AddCell(dateCell);
                var cellsHeadings = this.CreateTableRow(this.defaultCellsHeadings, 1, this.defaultColumnsHeadingsColor);
                dataTable.Rows.Add(cellsHeadings);
                foreach (var sale in date.Sales)
                {
                    var row = this.CreateTableRow(sale, 1);
                    dataTable.Rows.Add(row);
                }

                this.CreateTotalRow(date.Date, date.TotaSum, ref dataTable);
            }
        }

        private void AddGrandTotalRow(
            string grandTotalRowHeading, decimal grandTotalSum, BaseColor backgroudColor, ref PdfPTable dataTable)
        {
            var totalRowHeading = this.CreateCell(
                this.defaultAggregatedRowFont, 4, backgroudColor, 2, grandTotalRowHeading);
            var totalRowValue = this.CreateCell(
                this.defaultAggregatedRowFont, 1, backgroudColor, 2, grandTotalSum.ToString());
            dataTable.AddCell(totalRowHeading);
            dataTable.AddCell(totalRowValue);
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
                var cell = this.CreateCell(this.defaultCellFont, 1, cellsAlignment, property.GetValue(rowData, null).ToString());
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
                var cell = this.CreateCell(this.defaultCellFont, 1, cellsAlignment, cellData.ToString());
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
                this.defaultAggregatedRowFont,
                4,
                2,
                string.Format(DefaultTotalRowHeading, date.ToString(DefaultDateFormat)));
            var totalRowValue = this.CreateCell(
                this.defaultAggregatedRowFont,
                1,
                2,
                totalSum.ToString());
            dataTable.AddCell(totalRowHeading);
            dataTable.AddCell(totalRowValue);
        }
    }
}
