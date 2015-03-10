namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
        private IDictionary<DateTime, IList<object>> RetrieveAggregateSalesInformation(
            IProductsSystemData data, string startDate, string endDate)
        {
            var aggregatedSalesData = new Dictionary<DateTime, IList<object>>
            {
                {
                    DateTime.ParseExact("20-07-2014", "dd-mm-yyyy", CultureInfo.InvariantCulture),
                    new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    }
                },
                {
                    DateTime.ParseExact("21-07-2014", "dd-mm-yyyy", CultureInfo.InvariantCulture),
                    new List<object>
                    {
                        new {Product = "Beer “Beck’s”", Quantity = "40 litets", UnitPrice = 1.20m, Location = "Supermarket “Kaspichan – Center”", Sum = 50.00},
                        new {Product = "Beer “Zagorka”", Quantity = "37 litets", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 58.00}
                    }
                },
                {
                    DateTime.ParseExact("22-07-2014", "dd-mm-yyyy", CultureInfo.InvariantCulture),
                    new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "16 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 65.00},
                        new {Product = "Beer “Zagorka”", Quantity = "90 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 70.00}
                    }
                },
            };

            return aggregatedSalesData;
        }
    }
}
