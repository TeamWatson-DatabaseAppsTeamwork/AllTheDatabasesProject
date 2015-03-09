namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using ProductsSystem.Data.Data;
    using PDFExporter;

    public class ExportPDFFileCommand : IEngineCommand
    {
        private PDFExporter exportExecutor;

        public ExportPDFFileCommand(IList<object> arguments, PDFExporter exportExecutor)
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
                        new {Product = "Beer “Zagorka”", Quantity = 11, Measure = "liters", UnitPrice = 1.00, Location = "Supermarket “Bourgas – Plaza”"},
                        new {Product = "Beer “Zagorka”", Quantity = 78, Measure = "liters", UnitPrice = 0.92, Location = "Supermarket “Kaspichan – Center”"}
                    }
                },
                {
                    DateTime.ParseExact("21-07-2014", "dd-mm-yyyy", CultureInfo.InvariantCulture),
                    new List<object>
                    {
                        new {Product = "Beer “Beck’s”", Quantity = 40, Measure = "liters", UnitPrice = 1.20, Location = "Supermarket “Kaspichan – Center”"},
                        new {Product = "Beer “Zagorka”", Quantity = 37, Measure = "liters", UnitPrice = 1.00, Location = "Supermarket “Bourgas – Plaza”"}
                    }
                },
                {
                    DateTime.ParseExact("22-07-2014", "dd-mm-yyyy", CultureInfo.InvariantCulture),
                    new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = 16.00, Measure = "liters", UnitPrice = 1.00, Location = "Supermarket “Bourgas – Plaza”"},
                        new {Product = "Beer “Zagorka”", Quantity = 90.00, Measure = "liters", UnitPrice = 0.92, Location = "Supermarket “Kaspichan – Center”"}
                    }
                },
            };

            return aggregatedSalesData;
        }
    }
}
