namespace ProductsSystem.DataTransferObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    [XmlType(TypeName = "sale")]
    public class SalesAggregated
    {
        public SalesAggregated()
            : this(string.Empty)
        { 
        }

        public SalesAggregated(string vendorName)
        {
            this.VendorName = vendorName;
            this.RawSummaries = new List<SalesSummary>();
        }

        [XmlAttribute("vendor")]
        public string VendorName { get; set; }

        [XmlIgnore]
        public IEnumerable<SalesSummary> RawSummaries { get; set; }

        [XmlArray("summaries")]
        public List<SalesSummary> Summaries
        {
            get
            {
                return this.RawSummaries.ToList();
            }
        }
    }
}
