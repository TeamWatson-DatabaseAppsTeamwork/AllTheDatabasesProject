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

        public override int GetHashCode()
        {
            return this.ProductId ^ this.SupermarketId;
        }

        public override bool Equals(object obj)
        {
            var saleComparator = obj as Sale;
            bool areEqual =
                (saleComparator != null) &&
                (saleComparator.ProductId == this.ProductId) &&
                (saleComparator.SupermarketId == this.SupermarketId);
            return areEqual;
        }
    }
}
