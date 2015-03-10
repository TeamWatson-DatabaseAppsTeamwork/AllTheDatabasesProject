namespace ProductsSystem.Engine
{
    public static class EngineConstants
    {
        // Common constants
        public const string DateFormat = "dd-MM-yyyy";
        public const string UserInputSplitSymbol = " ";

        // Commands
        public const string ExportPDFFile = "export-pdf";
        public const string ExportXMLFile = "export-xml";
        public const string Exit = "exit";

        // Exeptions messages
        public const string MissingCommandArgumentsMessage =
            "Arguments required for the command execution currently missing";
    }
}
