namespace ProductsSystem.Engine.EngineCommands
{
    using System.Collections;
    using System.Collections.Specialized;
    using ProductsSystem.Data.Data;

    using XmlImporter;

    public class ImportXmlFileCommand : IEngineCommand
    {
        private const int CommandArgumentsCount = 1;
        private XmlExpensesImporter xmlImporter;

        public ImportXmlFileCommand(XmlExpensesImporter xmlImporter)
        {
            this.xmlImporter = xmlImporter;
            this.Arguments = new string[CommandArgumentsCount];
        }

        public IList Arguments{ get; private set; }

        public string Execute(IProductsSystemData data)
        {
            throw new System.NotImplementedException();
        }

        public void RecieveArguments(string[] rawArguments)
        {
            
        }
    }
}
