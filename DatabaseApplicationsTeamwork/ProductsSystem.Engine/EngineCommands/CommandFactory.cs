namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using PdfExporter;

    public static class CommandFactory
    {
        public static IEngineCommand CreateCommand(Type type)
        {
            if (type == typeof(ExportPdfFileCommand))
            {
                return CreateExportPDFFileCommand();
            }
            //else if (type == typeof(ExportXMLFileCommand))
            //{
                return CreateExportXMLFileCommand();
            //}
        }

        private static ExportPdfFileCommand CreateExportPDFFileCommand()
        {
            var pdfExporter = new PdfSalesExporter();
            var createExportPdfFileCommand = new ExportPdfFileCommand(pdfExporter);
            return createExportPdfFileCommand;
        }

        private static ExportXmlFileCommand CreateExportXMLFileCommand()
        {
            var exportXMLFileCommand = new ExportXmlFileCommand();
            return exportXMLFileCommand;
        }
    }
}
