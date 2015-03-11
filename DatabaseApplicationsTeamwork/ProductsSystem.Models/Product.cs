﻿namespace ProductsSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int VendorId { get; set; }

        public Vendor Vendor { get; set; }

        public int MeasureId { get; set; }

        public Measure Measure { get; set; }
    }
}
