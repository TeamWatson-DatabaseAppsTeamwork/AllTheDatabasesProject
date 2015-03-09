namespace PDFExporter
{
    public interface IPDFExporter
    {
        string FileFolderPath { get; }

        string FileName { get; }

        void Export();

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
