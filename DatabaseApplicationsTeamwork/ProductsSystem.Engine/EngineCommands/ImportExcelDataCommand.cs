namespace ProductsSystem.Engine.EngineCommands
{
    using System;
    using ExcelImporter;
    using ProductsSystem.Data.Data;

    public class ImportExcelDataCommand : IEngineCommand
    {
        private IExcelImporter excelImporter;

        public ImportExcelDataCommand(IExcelImporter excelImporter)
        {
            this.excelImporter = excelImporter;
        }

        public System.Collections.IList Arguments { get; private set; }

        public string Execute(IProductsSystemData data)
        {
            Console.WriteLine("Importing Excel data...test");
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
            
        }
    }
}
