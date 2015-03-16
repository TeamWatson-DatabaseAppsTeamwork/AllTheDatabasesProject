namespace ProductsSystem.DataTransferObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    public class SalesByProduct
    {

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string VendorName { get; set; }

        public int TotalQuantitySold { get; set; }

        public decimal TotalIncomes { get; set; }
    }
}
