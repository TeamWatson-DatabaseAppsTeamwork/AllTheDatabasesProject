namespace ProductsSystem.Engine.EngineCommands
{
    using System.Collections.Generic;
    using ProductsSystem.Data.Data;

    public interface IEngineCommand
    {
        IList<object> Arguments { get; }

        string Execute(IProductsSystemData data);
    }
}
