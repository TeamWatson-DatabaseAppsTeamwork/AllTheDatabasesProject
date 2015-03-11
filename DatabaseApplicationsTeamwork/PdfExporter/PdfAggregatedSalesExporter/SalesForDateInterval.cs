namespace PdfExporter.PdfAggregatedSalesExporter
{
    using System;
    using System.Collections;

    public struct SalesForDateInterval
    {
        public DateTime Date { get; set; }

        public IList Sales { get; set; }

        public decimal TotaSum { get; set; }
    }
}
