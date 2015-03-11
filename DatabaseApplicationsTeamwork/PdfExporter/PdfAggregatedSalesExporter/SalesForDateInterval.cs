namespace PdfExporter.PdfAggregatedSalesExporter
{
    using System;
    using System.Collections.Generic;

    public struct SalesForDateInterval
    {
        public DateTime Date { get; set; }

        public IList<object> Sales { get; set; }

        public decimal TotaSum { get; set; }
    }
}
