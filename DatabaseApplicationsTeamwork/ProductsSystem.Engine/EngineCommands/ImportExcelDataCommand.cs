﻿namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using Bytescout.Spreadsheet;
    using ExcelImporter;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Models;

    public class ImportExcelDataCommand : IEngineCommand
    {
        private const string StringDateFormat = "dd-MMM-yyyy";
        private static readonly string DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private const string DefaultZipName = "Sales-Reports.zip";
        private const string DefaultExtractDirectory = "Sales-Reports";
        private static readonly string DefaultZipPath = Path.Combine(DefaultDirectory, DefaultZipName);
        private static readonly string DefaultExtractPath = Path.Combine(DefaultDirectory, DefaultExtractDirectory);
        private static readonly WorksheetSettings WorksheetSettings =
            new WorksheetSettings
            {
                StartCell = 1,
                StartCollumn = 1,
                StartRow = 1,
                EndRowContent = "Total sum:",
                FirstContentRow = 3,
                ProductCell = 1,
                QuantityCell = 2
            };

        private IExcelImporter excelImporter;

        public ImportExcelDataCommand(IExcelImporter excelImporter)
        {
            this.excelImporter = excelImporter;
        }

        public System.Collections.IList Arguments { get; private set; }

        public string Execute(IProductsSystemData data)
        {
            var sales = this.LoadSales(data);
            excelImporter.DataToBeImported = sales;
            excelImporter.Import(data);
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
        }

        private IList<Sale> LoadSales(IProductsSystemData data)
        {
            ZipFile.ExtractToDirectory(DefaultZipPath, DefaultExtractPath);
            var salesReportsDirectories = Directory.GetDirectories(DefaultExtractPath);
            var sales = new List<Sale>();
            foreach (var directory in salesReportsDirectories)
            {
                var salesDate = Path.GetFileName(directory);
                var date = DateTime.ParseExact(salesDate, StringDateFormat, CultureInfo.InvariantCulture);
                var salesReports = Directory.GetFiles(directory);
                foreach (var salesReport in salesReports)
                {
                    sales.AddRange(this.ExtractExcelFileData(salesReport, date, data));
                }
            }
            
            return sales;
        }

        private IList<Sale> ExtractExcelFileData(string file, DateTime date, IProductsSystemData data)
        {
            IList<Sale> sales = new List<Sale>();
            var report = new Spreadsheet();
            using (report)
            {
                report.LoadFromFile(file);
                Worksheet worksheet = report.Workbook.Worksheets.ByName("Sales");
                var supermarketName = worksheet.Cell(WorksheetSettings.StartRow, WorksheetSettings.StartCell).Value;
                int supermarketId = data.Supermarkets.Find(s => s.Location == supermarketName).Select(s => s.Id).First();
                string productName;
                int productId;
                int quantity;
                int currentRow = WorksheetSettings.FirstContentRow;
                string checkContent = worksheet.Cell(currentRow, WorksheetSettings.ProductCell).ValueAsString;
                while (checkContent != WorksheetSettings.EndRowContent)
                {
                    productName = worksheet.Cell(currentRow, WorksheetSettings.ProductCell).ValueAsString;
                    productId = data.Products.Find(p => p.Name == productName).Select(p => p.Id).First();
                    quantity = worksheet.Cell(currentRow, WorksheetSettings.QuantityCell).ValueAsInteger;
                    var sale = new Sale
                    {
                        ProductId = productId,
                        SupermarketId = supermarketId,
                        Quantity = quantity,
                        Date = date
                    };
                    sales.Add(sale);
                    currentRow++;
                    checkContent = worksheet.Cell(currentRow, WorksheetSettings.ProductCell).ValueAsString;
                }
            }

            return sales;
        }
    }
}
