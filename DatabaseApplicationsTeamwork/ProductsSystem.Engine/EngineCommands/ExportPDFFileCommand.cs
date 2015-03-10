namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using PDFExporter.PDFAggregatedSalesExporter;
    using ProductsSystem.Data.Data;
    using PDFExporter;

    public class ExportPDFFileCommand : IEngineCommand
    {
        private PDFSalesExporter pdfExporter;

        public ExportPDFFileCommand(PDFSalesExporter pdfExporter)
        {
            this.pdfExporter = pdfExporter;
            this.Arguments = new List<DateTime>();
        }

        public IList Arguments { get; private set; }

        /// <summary>
        /// Generate a pdf report with the data retrieved
        /// </summary>
        /// <param name="data">Data source</param>
        /// <returns>Message for successful document generation</returns>
        /// <exception cref="InvalidOperationException">
        /// Throws exeption when no command arguments are available
        /// to process the command</exception>
        public string Execute(IProductsSystemData data)
        {
            if (this.Arguments.Count > 0)
            {
                var aggregatedSalesData = this.RetrieveAggregateSalesInformation(data, "", "");
                pdfExporter.Data = aggregatedSalesData;
                pdfExporter.Export();
                this.Arguments.Clear();
                // TODO Successful pdf generation message
                return "";
            }

            throw new InvalidOperationException(EngineConstants.MissingCommandArgumentsMessage);
        }

        /// <summary>
        /// Revieves users search parameters - start date and end date as string array
        /// Parses the dates using specified date format and stores them
        /// </summary>
        /// <param name="rawArguments">
        /// String array holding users search parameters for retrieving sales data</param>
        public void RecieveArguments(string[] rawArguments)
        {
            var startDate = DateTime.ParseExact(rawArguments[0], EngineConstants.DateFormat, CultureInfo.InvariantCulture);
            var endDate = DateTime.ParseExact(rawArguments[1], EngineConstants.DateFormat, CultureInfo.InvariantCulture);
            this.Arguments.Add(startDate);
            this.Arguments.Add(endDate);
        }

        private IList<SalesForDate> RetrieveAggregateSalesInformation(
            IProductsSystemData data, string startDate, string endDate)
        {
            // TODO query to retrive data from the data repository
            // Currently implemented with test data

            var aggregatedSalesData = new List<SalesForDate>
            {
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", EngineConstants.DateFormat, CultureInfo.InvariantCulture),
                    Sales = new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    },
                    TotaSum = 850
                },
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", EngineConstants.DateFormat, CultureInfo.InvariantCulture),
                    Sales = new List<object>
                    {
                        new {Product = "Beer “Zagorka”", Quantity = "11 liters", UnitPrice = 1.00m, Location = "Supermarket “Bourgas – Plaza”", Sum = 48.00m},
                        new {Product = "Beer “Zagorka”", Quantity = "78 liters", UnitPrice = 0.92m, Location = "Supermarket “Kaspichan – Center”", Sum = 37.00}
                    },
                    TotaSum = 850
                },
                new SalesForDate
                {
                    Date = DateTime.ParseExact("20-07-2014", EngineConstants.DateFormat, CultureInfo.InvariantCulture),
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
