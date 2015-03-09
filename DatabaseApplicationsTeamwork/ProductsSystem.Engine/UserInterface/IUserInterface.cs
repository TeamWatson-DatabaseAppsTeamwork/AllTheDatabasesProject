namespace ProductsSystem.Engine
{
    public interface IUserInterface
    {
        string Input { get; }

        string Output { get; }

        string Read();

        void Write();
    }
}
