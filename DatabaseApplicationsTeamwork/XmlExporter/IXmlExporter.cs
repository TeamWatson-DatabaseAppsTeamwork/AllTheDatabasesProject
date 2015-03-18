namespace XmlExporter
{
    using System.Collections.Generic;

    using ProductsSystem.DataTransferObjects;

    public interface IXmlExporter
    {
        string FileFolderPath { get; }

        string FileName { get; }

        void Export(IList<SalesAggregated> aggregatedSalesData);

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
