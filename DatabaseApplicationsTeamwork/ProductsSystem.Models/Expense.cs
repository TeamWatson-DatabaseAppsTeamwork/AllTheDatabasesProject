namespace ProductsSystem.Models
{
    using System;

    public class Expense
    {
        public int Id { get; set; }

        public int VendorID { get; set; }

        public virtual Vendor Vendor { get; set; }

        public DateTime Period { get; set; }

        public decimal Amount { get; set; }
    }
}
