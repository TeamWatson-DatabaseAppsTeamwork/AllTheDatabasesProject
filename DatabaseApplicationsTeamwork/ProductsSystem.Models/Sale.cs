namespace ProductsSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public int Id { get; set; }

        [ForeignKey("Supermarket")]
        public int SupermarketId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supermarket Supermarket { get; set; }
    }
}
