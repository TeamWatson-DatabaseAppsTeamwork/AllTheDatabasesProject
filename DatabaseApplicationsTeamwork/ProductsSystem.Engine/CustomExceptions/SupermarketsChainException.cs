namespace ProductsSystem.Engine.CustomExceptions
{
    using System;

    public class SupermarketsChainException : Exception
    {
        public SupermarketsChainException(string message)
            : base(message)
        {
        }
    }
}
