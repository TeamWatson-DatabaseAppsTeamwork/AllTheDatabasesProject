namespace ProductsSystem.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProductsSystem.Data.Data;
    using ProductsSystem.Engine.CustomExceptions;
    using ProductsSystem.Engine.EngineCommands;
    using ProductsSystem.Engine.UserInterface;

    public class Engine
    {
        private static Engine instance;
        private readonly IUserInterface userInterface;
        private IProductsSystemData data;
        private IDictionary<Type, IEngineCommand> commands;
        private string output;

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
                try
                {
                    string userInputAsString = this.userInterface.Read();
                    var userInput = this.ParseCommand(userInputAsString);
                    string command = userInput[0];
                    if (command == EngineConstants.Exit)
                    {
                        break;
                    }

                    string[] commandArguments = userInput.Skip(1).ToArray();
                    this.InvokeCommand(command, commandArguments);
                }
                catch (SupermarketsChainException supermarketsChainException)
                {
                    this.output = supermarketsChainException.Message;
                    this.ShowOutputToUser();
                }
            }
        }

        private string[] ParseCommand(string userInputAsString)
        {
            if (!string.IsNullOrEmpty(userInputAsString))
            {
                var userInput = userInputAsString.Split(
                new string[] { EngineConstants.UserInputSplitSymbol }, StringSplitOptions.RemoveEmptyEntries);
                return userInput;
            }
            
            throw new SupermarketsChainException(EngineConstants.InvalidInputMessage);
        }

        private void InvokeCommand(string command, string[] arguments)
        {
            var currentCommand = this.GetCurrentCommand(command);
            currentCommand.RecieveArguments(arguments);
            this.output = currentCommand.Execute(this.data);
            this.ShowOutputToUser();
        }

        private IEngineCommand GetCurrentCommand(string command)
        {
            Type commandType = typeof(object);
            IEngineCommand currentCommand;

            switch (command)
            {
                case EngineConstants.ExportPdfFile:
                    commandType = typeof(ExportPdfFileCommand);
                    break;
                case EngineConstants.ExportXmlFile:
                    commandType = typeof(ExportXmlFileCommand);
                    break;
                case EngineConstants.ImportXmlFile:
                    commandType = typeof (ImportXmlFileCommand);
                    break;
                default:
                    throw new SupermarketsChainException(EngineConstants.InvalidCommandMessage);
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

        private void ShowOutputToUser()
        {
            this.userInterface.Write(this.output);
        }
    }
}
