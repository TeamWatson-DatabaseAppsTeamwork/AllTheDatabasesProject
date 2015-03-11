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
            "Arguments required for the command execution currently missing";

        public const string InvalidOperationMessage =
            "Invalid attempt to execute a command. Possible reason could be missing search arguments. Please try again.";

        public const string InvalidInputFormatMessage =
            "Please ensure that you are using the correct fomat for the input data.";

        public const string InvalidInputMessage =
            "Please ensure you have entered non-empty command";

            // Success messages
        public const string PdfReportSuccessfullyExportedMessage =
            "PDF report successfully exported";
    }
}
