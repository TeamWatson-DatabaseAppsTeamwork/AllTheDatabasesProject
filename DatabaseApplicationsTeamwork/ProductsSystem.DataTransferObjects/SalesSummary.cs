namespace ProductsSystem.DataTransferObjects
{
    using System;
    using System.Xml.Serialization;

    [XmlType(TypeName = "summary")]
    public class SalesSummary
    {
        public SalesSummary()
        {
        }

        public SalesSummary(DateTime date, decimal totalSum)
        {
            this.Date = date;
            this.TotalSum = totalSum;
        }

        [XmlAttribute("date")]
        public DateTime Date { get; set; }


        [XmlAttribute("total-sum")]
        public decimal TotalSum { get; set; }
    }
}
