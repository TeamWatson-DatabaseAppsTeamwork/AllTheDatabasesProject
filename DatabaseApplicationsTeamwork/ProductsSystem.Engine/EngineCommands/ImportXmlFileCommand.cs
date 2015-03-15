namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine.CustomExceptions;
    using XmlImporter;

    public class ImportXmlFileCommand : IEngineCommand
    {
        private readonly string DefaultReportsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private const int CommandArgumentsCount = 1;
        private XmlExpensesImporter xmlImporter;

        public ImportXmlFileCommand(XmlExpensesImporter xmlImporter)
        {
            this.xmlImporter = xmlImporter;
            this.Arguments = new string[CommandArgumentsCount];
        }

        public IList Arguments{ get; private set; }

        public string Execute(IProductsSystemData data)
        {
            var dataToBeImported = this.LoadDataToBeImported();
            xmlImporter.DataToBeImported = dataToBeImported;
            xmlImporter.Import(data);
            return EngineConstants.XmlReportSuccessfullyImportedMessage;
        }

        public void RecieveArguments(string[] rawArguments)
        {
            if (rawArguments.Count() == CommandArgumentsCount)
            {
                this.Arguments = rawArguments;
            }
            else
            {
                throw new SupermarketsChainException(EngineConstants.CommandArgumentsMissmatchMessage);
            }
        }

        private IDictionary<string, IList<KeyValuePair<DateTime, decimal>>> LoadDataToBeImported()
        {
            var xmlReport = new XmlDocument();
            var reportPath = DefaultReportsDirectory + Path.DirectorySeparatorChar + this.Arguments[0];
            try
            {
                xmlReport.Load(reportPath);
            }
            catch (FileNotFoundException)
            {
                throw new SupermarketsChainException(EngineConstants.FileNotFoundMessage);
            }

            var data = this.RetrieveData(xmlReport);

            return data;
        }

        private IDictionary<string, IList<KeyValuePair<DateTime, decimal>>> RetrieveData(XmlDocument xmlReport)
        {
            var data = new Dictionary<string, IList<KeyValuePair<DateTime, decimal>>>();
            var vendors = xmlReport.SelectNodes("/expenses-by-month/vendor");
            foreach (XmlElement vendor in vendors)
            {
                var vendorName = vendor.GetAttribute("name");
                data[vendorName] = new List<KeyValuePair<DateTime, decimal>>();
                var expenses = vendor.SelectNodes("expenses");
                foreach (XmlElement expense in expenses)
                {
                    data[vendorName]
                        .Add
                        (
                            new KeyValuePair<DateTime, decimal>
                            (
                                DateTime.ParseExact(expense.GetAttribute("month"), "MMM-yyyy", CultureInfo.InvariantCulture),
                                Decimal.Parse(expense.InnerText)
                            )
                        );
                }
            }

            return data;
        }
    }
}
