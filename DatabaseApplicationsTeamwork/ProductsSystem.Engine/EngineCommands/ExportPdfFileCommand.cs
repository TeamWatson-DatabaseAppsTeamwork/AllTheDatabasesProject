namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using PdfExporter.PdfAggregatedSalesExporter;

    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine.CustomExceptions;

    public class ExportPdfFileCommand : IEngineCommand
    {
        private const int CommandArgumentsCount = 2;
        private PdfSalesExporter pdfExporter;

        public ExportPdfFileCommand(PdfSalesExporter pdfExporter)
        {
            this.pdfExporter = pdfExporter;
            this.Arguments = new DateTime[CommandArgumentsCount];
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
            var commandOutput = "";

            var aggregatedSalesData = this.RetrieveAggregateSalesInformation(
                data, (DateTime)this.Arguments[0], (DateTime)this.Arguments[1]);
            if (aggregatedSalesData.Count == 0)
            {
                commandOutput = EngineConstants.NoResultDataMessage;
            }
            else
            {
                this.pdfExporter.Data = aggregatedSalesData;
                this.pdfExporter.Export(); 
                commandOutput = EngineConstants.PdfReportSuccessfullyExportedMessage;
            }

            this.Arguments.Clear();
            return commandOutput;
        }

        /// <summary>
        /// Revieves users search parameters - start date and end date as string array
        /// Parses the dates using specified date format and stores them
        /// </summary>
        /// <param name="rawArguments">
        /// String array holding users search parameters for retrieving sales data</param>
        public void RecieveArguments(string[] rawArguments)
        {
            if (rawArguments.Count() == CommandArgumentsCount)
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
            else
            {
                throw new SupermarketsChainException(EngineConstants.CommandArgumentsMissmatchMessage);
            }
        }

        private IList<SalesForDateInterval> RetrieveAggregateSalesInformation(
            IProductsSystemData data, DateTime startDate, DateTime endDate)
        {
            var dataFiltered = data.Sales
                .Find(s => s.Date >= startDate && s.Date <= endDate)
                .GroupBy(s => s.Date)
                .ToDictionary(group => group.Key, group => group.ToList());

            var aggregatedSalesData = new List<SalesForDateInterval>();
            foreach (var group in dataFiltered)
            {
                var sales = group.Value;

                aggregatedSalesData.Add(
                    new SalesForDateInterval
                        {
                        Date = group.Key,
                        Sales = group.Value.Select(sale =>
                            new
                            {
                                Product = sale.Product.Name,
                                Quantity = sale.Quantity + " " + sale.Product.Measure.Name,
                                UnitPrice = sale.Product.Prices.First(p => p.SupermarketId == sale.SupermarketId).PriceValue,
                                Location = sale.Supermarket.Location,
                                Sum = sale.Quantity * sale.Product.Price
                            }).ToList(),
                        TotaSum = group.Value.Sum(s => (s.Quantity * s.Product.Price))
                    });
            }

            return aggregatedSalesData;
        }
    }
}
