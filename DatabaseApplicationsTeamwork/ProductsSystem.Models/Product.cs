namespace ProductsSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public Product()
        {
            this.Prices = new HashSet<Price>();
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Vendor")]
        public int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        [ForeignKey("Measure")]
        public int MeasureId { get; set; }

        [ForeignKey("MeasureId")]
        public virtual Measure Measure { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public virtual ICollection<Price> Prices { get; set; } 
    }
}
