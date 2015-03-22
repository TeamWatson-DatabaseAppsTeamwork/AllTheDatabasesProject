namespace ProductsSystem.Engine
{
    using System;

    public static class EngineConstants
    {
        // Common constants
        public const string DateFormat = "dd-MM-yyyy";
        public const string UserInputSplitSymbol = " ";

        // Commands
        public const string ExportPdfFile = "export-pdf";
        public const string ExportXmlFile = "export-xml";
        public const string ExportJsonFile = "export-json";
        public const string ImportXmlFile = "import-xml";
        public const string ImportOracleDataToSqlServer = "import-oracle-data";
        public const string ImportExcelData = "import-excel-data";
        public const string Exit = "exit";

        // Messages
            // Start message
        public static readonly string StartMessage =
            "Commands available" + Environment.NewLine +
            "  -" + ExportPdfFile + " <start date> <end date>" + Environment.NewLine +
            "  -" + ExportXmlFile + " <start date> <end date>" + Environment.NewLine +
            "  -" + ExportJsonFile + " <start date> <end date>" + Environment.NewLine +
            "  -" + ImportXmlFile + Environment.NewLine +
            "  -" + ImportOracleDataToSqlServer + Environment.NewLine +
            "  -" + ImportExcelData + Environment.NewLine +
            "  -" + Exit + Environment.NewLine +
            "System is waiting for your command";

            // Error messages
        public const string CommandArgumentsMissmatchMessage =
            "Arguments count do not match the count needed for the command execution";

        public const string InvalidInputFormatMessage =
            "Please ensure that you are using the correct fomat for the input data.";

        public const string InvalidCommandMessage = "Invalid command. Please try again.";

        public const string InvalidInputMessage =
            "Please ensure you have entered non-empty command.";

        public const string NoResultDataMessage = "No data found to be exported. Please try again.";

        public const string FileNotFoundMessage = "File needed is not found";

        public const string InvaliDateRangeMessage = "The date range is invalid. Please try again.";

            // Success messages
        public const string PdfReportSuccessfullyExportedMessage =
            "PDF report successfully exported.";

        public const string XmlReportSuccessfullyExportedMessage =
            "XML report successfully exported.";

        public const string JsonReportSuccessfullyExportedMessage =
            "JSON report successfully exported.";

        public const string XmlReportSuccessfullyImportedMessage =
            "XML report successfully imported.";

        public const string OracleDataSuccessfullyImported =
            "Oracle data for products successfully imported into SQL Server";

        public const string ExcelDataSuccessfullyImported =
            "Excel data for sales successfully imported";
    }
}
