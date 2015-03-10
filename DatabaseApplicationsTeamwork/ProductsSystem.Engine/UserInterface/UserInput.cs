namespace ProductsSystem.Engine
{
    using System.Collections;

    public struct UserInput
    {
        public string Command { get; private set; }

        public IList Arguments { get; private set; } 
    }
}
