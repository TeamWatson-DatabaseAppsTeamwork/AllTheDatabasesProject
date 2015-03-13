namespace ProductsSystem.DataTransferObjects
{
    using System;

    public class SalesAggregated
    {
        public string VendorName { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalSum { get; set; }
    }
}
