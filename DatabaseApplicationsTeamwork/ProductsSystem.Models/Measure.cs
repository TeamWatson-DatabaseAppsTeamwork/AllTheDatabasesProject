namespace ProductsSystem.Models
{
    public class Measure
    {
        private string name;

        public int Id { get; set; }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
