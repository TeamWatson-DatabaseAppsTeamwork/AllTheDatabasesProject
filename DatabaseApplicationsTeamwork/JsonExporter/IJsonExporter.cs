namespace JsonExporter
{
    using System.Collections.Generic;

    using ProductsSystem.DataTransferObjects;

    public interface IJsonExporter
    {
        string FileFolderPath { get; }

        string FileName { get; }

        void Export(IList<SalesByProduct> aggregatedSalesData);

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
