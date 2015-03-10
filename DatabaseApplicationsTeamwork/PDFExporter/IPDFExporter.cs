namespace PDFExporter
{
    public interface IPDFExporter
    {
        string FileFolderPath { get; }

        string FileName { get; }

        int ColumnsNumber { get; set; }

        void Export();

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
