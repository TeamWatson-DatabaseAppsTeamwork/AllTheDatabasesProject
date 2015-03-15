namespace ProductsSystem.Engine.EngineCommands
{
    using System;

    using JsonExporter.JsonAggregatedSalesExporter;

    using PdfExporter.PdfAggregatedSalesExporter;
    using XmlExporter.XmlAggregatedSalesExporter;
    using XmlImporter;

    public static class CommandFactory
    {
        public static IEngineCommand CreateCommand(Type type)
        {
            if (type == typeof(ExportPdfFileCommand))
            {
                return CreateExportPdfFileCommand();
            }
            else if (type == typeof(ExportXmlFileCommand))
            {
                return CreateExportXmlFileCommand();
            }
            else if (type == typeof(ExportJsonFileCommand))
            {
                return CreateExportJsonFileCommand();
            }
            //else if (type == typeof(ImportXmlFileCommand))
            //{
                return CreateImportXmlFileCommand();
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

        private static ExportJsonFileCommand CreateExportJsonFileCommand()
        {
            var jsonExporter = new JsonSalesExporter();
            var exportJsonFileCommand = new ExportJsonFileCommand(jsonExporter);
            return exportJsonFileCommand;
        }

        private static ImportXmlFileCommand CreateImportXmlFileCommand()
        {
            var xmlImporter = new XmlExpensesImporter();
            var importXmlFileCommand = new ImportXmlFileCommand(xmlImporter);
            return importXmlFileCommand;
        }
    }
}
