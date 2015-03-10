namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using PDFExporter;

    public static class CommandFactory
    {
        public static IEngineCommand CreateCommand(Type type)
        {
            if (type == typeof(ExportPDFFileCommand))
            {
                return CreateExportPDFFileCommand();
            }
            //else if (type == typeof(ExportXMLFileCommand))
            //{
                return CreateExportXMLFileCommand();
            //}
        }

        private static ExportPDFFileCommand CreateExportPDFFileCommand()
        {
            var pdfExporter = new PDFSalesExporter();
            var createExportPdfFileCommand = new ExportPDFFileCommand(pdfExporter);
            return createExportPdfFileCommand;
        }

        private static ExportXMLFileCommand CreateExportXMLFileCommand()
        {
            var exportXMLFileCommand = new ExportXMLFileCommand();
            return exportXMLFileCommand;
        }
    }
}
