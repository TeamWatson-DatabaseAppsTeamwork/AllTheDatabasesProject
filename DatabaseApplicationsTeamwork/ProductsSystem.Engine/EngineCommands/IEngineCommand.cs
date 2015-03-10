namespace ProductsSystem.Engine.EngineCommands
{
    using System.Collections;
    using ProductsSystem.Data.Data;

    public interface IEngineCommand
    {
        IList Arguments { get; }

        string Execute(IProductsSystemData data);

        void RecieveArguments(string[] rawArguments);
    }
}
