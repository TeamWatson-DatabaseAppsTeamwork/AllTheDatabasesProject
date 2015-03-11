namespace ProductsSystem.Engine.EngineCommands
{
    using System;

    public class ExportXMLFileCommand : IEngineCommand
    {

        public System.Collections.IList Arguments
        {
            get { throw new NotImplementedException(); }
        }

        public string Execute(Data.Data.IProductsSystemData data)
        {
            Console.WriteLine("Exporting XML...");
            return "";
        }

        public void RecieveArguments(string[] rawArguments)
        {
            // throw new NotImplementedException();
        }
    }
}
