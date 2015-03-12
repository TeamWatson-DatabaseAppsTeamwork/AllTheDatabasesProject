namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using PdfExporter;

    using XmlExporter.XmlAggregatedSalesExporter;

    public static class CommandFactory
    {
        public static IEngineCommand CreateCommand(Type type)
        {
            if (type == typeof(ExportPdfFileCommand))
            {
                return CreateExportPdfFileCommand();
            }
            //else if (type == typeof(ExportXMLFileCommand))
            //{
                return CreateExportXmlFileCommand();
            //}
        }

        private static ExportPdfFileCommand CreateExportPdfFileCommand()
        {
            var pdfExporter = new PdfSalesExporter();
            var createExportPdfFileCommand = new ExportPdfFileCommand(pdfExporter);
            return createExportPdfFileCommand;
        }

        private static ExportXmlFileCommand CreateExportXmlFileCommand()
        {
            var xmlExporter = new XmlSalesExporter();
            var exportXmlFileCommand = new ExportXmlFileCommand(xmlExporter);
            return exportXmlFileCommand;
        }
    }
}
