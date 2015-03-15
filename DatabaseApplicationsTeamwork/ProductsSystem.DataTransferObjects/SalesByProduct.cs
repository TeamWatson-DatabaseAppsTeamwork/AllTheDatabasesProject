namespace ProductsSystem.DataTransferObjects
{
    using Newtonsoft.Json;

    public class SalesByProduct
    {
        [JsonProperty(PropertyName = "product-id")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "product-name")]
        public string ProductName { get; set; }

        [JsonProperty(PropertyName = "vendor-name")]
        public string VendorName { get; set; }

        [JsonProperty(PropertyName = "total-quantity-sold")]
        public int TotalQuantitySold { get; set; }

        [JsonProperty(PropertyName = "total-incomes")]
        public decimal TotalIncomes { get; set; }
    }
}
