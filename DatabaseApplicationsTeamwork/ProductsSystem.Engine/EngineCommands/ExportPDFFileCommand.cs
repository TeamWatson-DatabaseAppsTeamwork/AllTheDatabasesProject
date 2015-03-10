namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using PDFExporter.PDFAggregatedSalesExporter;
    using ProductsSystem.Data.Data;
    using PDFExporter;

    public class ExportPDFFileCommand : IEngineCommand
    {
        private PDFSalesExporter exportExecutor;

        public ExportPDFFileCommand(IList<object> arguments, PDFSalesExporter exportExecutor)
        {
            this.Arguments = arguments;
            this.exportExecutor = exportExecutor;
        }

        public IList<object> Arguments { get; private set; }

        public string Execute(IProductsSystemData data)
        {
            var aggregatedSalesData = this.RetrieveAggregateSalesInformation(data, "", "");
            exportExecutor.Data = aggregatedSalesData;
            exportExecutor.Export();

            return "";
        }

        // Test implementation of the method
        private IList<SalesForDate> RetrieveAggregateSalesInformation(
            IProductsSystemData data, string startDate, string endDate)
        {
            var aggregatedSalesData = new List<SalesForDate>
            {
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Sales = new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    },
                    TotaSum = 850
                },
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Sales = new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    },
                    TotaSum = 850
                },
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    Sales = new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    },
                    TotaSum = 850
                }
            };

            return aggregatedSalesData;
        }
    }
}
