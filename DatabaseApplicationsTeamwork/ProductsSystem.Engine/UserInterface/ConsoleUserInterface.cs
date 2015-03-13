namespace ProductsSystem.Engine.UserInterface
{
    using System;

    public class ConsoleUserInterface : IUserInterface
    {
        public string Input { get; private set; }

        public string Read()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }

        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}
