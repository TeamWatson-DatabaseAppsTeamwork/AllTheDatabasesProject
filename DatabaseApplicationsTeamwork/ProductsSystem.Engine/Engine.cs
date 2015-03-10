namespace ProductsSystem.Engine
{
    using ProductsSystem.Data.Data;
    using PDFExporter;
    using ProductsSystem.Engine.EngineCommands;

    public class Engine
    {
        private static Engine instance;
        private readonly IUserInterface userInterface;
        private IProductsSystemData data;

        private Engine(IUserInterface userInterface, IProductsSystemData data)
        {
            this.userInterface = userInterface;
            this.data = data;
        }

        public static Engine GetInstance(IUserInterface userInterface, IProductsSystemData data)
        {
            if (instance == null)
            {
                instance = new Engine(userInterface, data);
            }

            return instance;
        }

        public void Run()
        {
            while (true)
            {
                string userInputAsString = this.userInterface.Read();
                var userInput = this.ParseCommand(userInputAsString);

                if (userInput.Command == EngineConstants.Exit)
                {
                    break;
                }

                var pdfExecutor = new PDFSalesExporter();
                var command = new ExportPDFFileCommand(new [] {"", ""}, pdfExecutor);
                command.Execute(this.data);
            }
        }

        private UserInput ParseCommand(string userInputAsString)
        {
            var userInput = new UserInput();
            return userInput;
        }
    }
}
