namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using ProductsSystem.Data.Data;
    using ProductsSystem.DataTransferObjects;
    using ProductsSystem.Engine.CustomExceptions;

    using JsonExporter.JsonAggregatedSalesExporter;

    public class ExportJsonFileCommand : IEngineCommand
    {
        private const int CommandArgumentsCount = 2;
        private JsonSalesExporter jsonExporter;

        public ExportJsonFileCommand(JsonSalesExporter jsonExporter)
        {
            this.jsonExporter = jsonExporter;
            this.Arguments = new DateTime[2];
        }

        public IList Arguments { get; private set; }

        /// <summary>
        /// Generate a xml report with the data retrieved
        /// </summary>
        /// <param name="data">Data source</param>
        /// <returns>Message for successful document generation</returns>
        /// <exception cref="InvalidOperationException">
        /// Throws exeption when no command arguments are available
        /// to process the command</exception>
        public string Execute(IProductsSystemData data)
        {
            var commandOutput = "";

            if (this.Arguments.Count == CommandArgumentsCount)
            {
                IList<SalesByProduct> aggregatedSalesData;
                aggregatedSalesData = this.RetrieveAggregateSalesInformation(
                    data, (DateTime)this.Arguments[0], (DateTime)this.Arguments[1]);
                if (aggregatedSalesData.Count == 0)
                {
                    commandOutput = EngineConstants.NoResultDataMessage;
                }
                else
                {
                    this.jsonExporter.Data = aggregatedSalesData;
                    this.jsonExporter.Export(aggregatedSalesData);
                    commandOutput = EngineConstants.JsonReportSuccessfullyExportedMessage;
                }

                this.Arguments.Clear();
                return commandOutput;
            }

            throw new SupermarketsChainException(EngineConstants.InvalidCommandMessage);
        }

        /// <summary>
        /// Revieves users search parameters - start date and end date as string array
        /// Parses the dates using specified date format and stores them
        /// </summary>
        /// <param name="rawArguments">
        /// String array holding users search parameters for retrieving sales data</param>
        public void RecieveArguments(string[] rawArguments)
        {
            try
            {
                var startDate = DateTime.ParseExact(rawArguments[0], EngineConstants.DateFormat, CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(rawArguments[1], EngineConstants.DateFormat, CultureInfo.InvariantCulture);
                if (endDate <= startDate)
                {
                    throw new SupermarketsChainException(EngineConstants.InvaliDateRangeMessage);
                }

                this.Arguments[0] = startDate;
                this.Arguments[1] = endDate;
            }
            catch (FormatException)
            {
                throw new SupermarketsChainException(EngineConstants.InvalidInputFormatMessage);
            }
        }

        private IList<SalesByProduct> RetrieveAggregateSalesInformation(
            IProductsSystemData data, DateTime startDate, DateTime endDate)
        {
            var product = data.Sales.All()
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .GroupBy(s => s.Product)
                .Select(sp => new SalesByProduct
                {
                    ProductId = sp.Key.Id,
                    ProductName = sp.Key.Name,
                    VendorName = sp.Key.Vendor.Name,
                    TotalQuantitySold = sp.Sum(s => s.Quantity),
                    TotalIncomes = sp.Sum(s => s.Quantity * s.Product.Price),
                }).ToList();

            return product;
        }
    }
}
