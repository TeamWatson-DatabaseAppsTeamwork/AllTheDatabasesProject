namespace ProductsSystem.Engine.EngineCommands
{
    using System.Collections;
    using ProductsSystem.Data.Data;

    using XmlImporter;

    public class ImportXmlFileCommand : IEngineCommand
    {
        public ImportXmlFileCommand(XmlExpensesImporter xmlImporter)
        {
            
        }

        public IList Arguments
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Execute(IProductsSystemData data)
        {
            throw new System.NotImplementedException();
        }

        public void RecieveArguments(string[] rawArguments)
        {
            throw new System.NotImplementedException();
        }
    }
}
