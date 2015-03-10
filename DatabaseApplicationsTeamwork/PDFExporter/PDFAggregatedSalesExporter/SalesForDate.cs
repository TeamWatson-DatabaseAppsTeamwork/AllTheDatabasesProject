namespace PDFExporter.PDFAggregatedSalesExporter
{
    using System;
    using System.Collections.Generic;

    public struct SalesForDate
    {
        public DateTime Date { get; set; }

        public IList<object> Sales { get; set; }

        public decimal TotaSum { get; set; }
    }
}
