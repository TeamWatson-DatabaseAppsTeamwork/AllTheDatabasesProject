namespace ProductsSystem.Engine
{
    public interface IUserInterface
    {
        string Input { get; }

        string Read();

        void Write(string output);
    }
}
