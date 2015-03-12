namespace ProductsSystem.Engine
{
    public static class EngineConstants
    {
        // Common constants
        public const string DateFormat = "dd-MM-yyyy";
        public const string UserInputSplitSymbol = " ";

        // Commands
        public const string ExportPdfFile = "export-pdf";
        public const string ExportXmlFile = "export-xml";
        public const string Exit = "exit";

        // Messages
            // Error messages
        public const string MissingCommandArgumentsMessage =
            "Arguments required for the command execution currently missing.";

        public const string InvalidInputFormatMessage =
            "Please ensure that you are using the correct fomat for the input data.";

        public const string InvalidCommandMessage = "Invalid commnd. Please try again.";

        public const string InvalidInputMessage =
            "Please ensure you have entered non-empty command.";

        public const string NoResultDataMessage = "No data found to be exported. Please try again.";

        public const string InvaliDateRangeMessage = "The date range is invalid. Please try again.";

            // Success messages
        public const string PdfReportSuccessfullyExportedMessage =
            "PDF report successfully exported.";
    }
}
