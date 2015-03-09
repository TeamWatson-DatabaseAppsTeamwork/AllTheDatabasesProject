namespace ProductsSystem.Models
{
    public class Product
    {
        private string name;
        private decimal price;

        public int Id { get; set; }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }

        public int MeasureId { get; set; }

        public Measure Measure { get; set; }
    }
}
