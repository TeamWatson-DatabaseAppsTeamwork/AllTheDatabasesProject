namespace ProductsSystem.Engine
{
    using System.Collections.Generic;

    public struct UserInput
    {
        public string Command { get; private set; }

        public IList<object> Arguments { get; private set; } 
    }
}
