namespace ProductsSystem.Engine.UserInterface
{
    public interface IUserInterface
    {
        string Input { get; }

        string Read();

        void Write(string output);
    }
}
