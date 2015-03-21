namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using Bytescout.Spreadsheet;
    using ExcelImporter;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class ImportExcelDataCommand : IEngineCommand
    {
        private static readonly string DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private const string DefaultZipName = "Sales-Reports.zip";
        private const string DefaultExtractDirectory = "Sales-Reports";
        private static readonly string DefaultZipPath = Path.Combine(DefaultDirectory, DefaultZipName);
        private static readonly string DefaultExtractPath = Path.Combine(DefaultDirectory, DefaultExtractDirectory);
        private IExcelImporter excelImporter;

        public ImportExcelDataCommand(IExcelImporter excelImporter)
        {
            this.excelImporter = excelImporter;
        }

        public System.Collections.IList Arguments { get; private set; }

        public string Execute(IProductsSystemData data)
        {
            var sales = this.LoadSales();
            excelImporter.DataToBeImported = sales;
            excelImporter.Import(data);
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
        }

        private IList<Sale> LoadSales()
        {
            ZipFile.ExtractToDirectory(DefaultZipPath, DefaultExtractPath);
            var salesReportsDirectories = Directory.GetDirectories(DefaultExtractPath);
            foreach (var directory in salesReportsDirectories)
            {
                // var folderName = Path.GetFileName(directory);
                var salesReports = Directory.GetFiles(directory);
                foreach (var salesReport in salesReports)
                {
                    this.ExtractExcelFileData(salesReport);
                }
            }
            IList<Sale> sales = new List<Sale>();
            return sales;
        }

        private void ExtractExcelFileData(string file)
        {
            // Create new Spreadsheet
            var report = new Spreadsheet();
            using (report)
            {
                report.LoadFromFile(file);
                Worksheet worksheet = report.Workbook.Worksheets.ByName("Sales");
                var currentCell = worksheet.Cell(1, 1).Value;
                Console.WriteLine();
            }
        }
    }
}
