namespace ProductsSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Price
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Key, Column(Order = 2)]
        [ForeignKey("Supermarket")]
        public int SupermarketId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Supermarket Supermarket { set; get; }

        public decimal PriceValue { get; set; }

        public override int GetHashCode()
        {
            return this.ProductId ^ this.SupermarketId;
        }

        public override bool Equals(object obj)
        {
            var priceComparator = obj as Price;
            bool areEqual =
                (priceComparator != null) &&
                (priceComparator.ProductId == this.ProductId) &&
                (priceComparator.SupermarketId == this.SupermarketId);
            return areEqual;
        }
    }
}
