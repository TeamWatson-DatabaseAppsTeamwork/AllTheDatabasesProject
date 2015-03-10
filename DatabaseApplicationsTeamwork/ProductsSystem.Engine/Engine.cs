namespace ProductsSystem.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine.EngineCommands;

    public class Engine
    {
        private static Engine instance;
        private readonly IUserInterface userInterface;
        private IProductsSystemData data;
        private IDictionary<Type, IEngineCommand> commands; 

        private Engine(IUserInterface userInterface, IProductsSystemData data)
        {
            this.userInterface = userInterface;
            this.data = data;
            this.commands = new Dictionary<Type, IEngineCommand>();
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
                string command = userInput[0];
                string[] commandArguments = userInput.Skip(1).ToArray();
                this.InvokeCommand(command, commandArguments);
            }
        }

        private string[] ParseCommand(string userInputAsString)
        {
            var userInput = userInputAsString.Split(
                new string[]{EngineConstants.UserInputSplitSymbol}, StringSplitOptions.RemoveEmptyEntries);
            return userInput;
        }

        private void InvokeCommand(string command, string[] arguments)
        {
            var currentCommand = this.GetCurrentCommand(command);
            currentCommand.RecieveArguments(arguments);
            currentCommand.Execute(this.data);
        }

        private IEngineCommand GetCurrentCommand(string command)
        {
            Type commandType = typeof(object);
            IEngineCommand currentCommand;

            switch (command)
            {
                case EngineConstants.ExportPDFFile:
                    commandType = typeof (ExportPDFFileCommand);
                    break;
                case EngineConstants.ExportXMLFile:
                    commandType = typeof(ExportXMLFileCommand); break;
            }

            currentCommand = this.PullCommand(commandType);
            return currentCommand;
        }

        private IEngineCommand PullCommand(Type commandType)
        {
            if (!this.commands.ContainsKey(commandType))
            {
                this.commands.Add(commandType, CommandFactory.CreateCommand(commandType));
            }

            return this.commands[commandType];
        }
    }
}
