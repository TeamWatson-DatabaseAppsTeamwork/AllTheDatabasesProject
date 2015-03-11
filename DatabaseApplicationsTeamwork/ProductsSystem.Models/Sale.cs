namespace ProductsSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        [Key, Column(Order = 1)]
        public int SupermarketId { get; set; }

         [Key, Column(Order = 2)]
        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public Product Product { get; set; }

        public Supermarket Supermarket { get; set; } 
    }
}
