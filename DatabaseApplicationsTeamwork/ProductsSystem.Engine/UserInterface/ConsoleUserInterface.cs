namespace ProductsSystem.Engine
{
    using System;

    public class ConsoleUserInterface : IUserInterface
    {
        public string Input { get; private set; }

        public string Output { get; private set; }

        public string Read()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }

        public void Write()
        {
            Console.WriteLine(this.Output);
        }
    }
}
