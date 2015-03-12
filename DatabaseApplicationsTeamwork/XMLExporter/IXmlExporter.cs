namespace XmlExporter
{
    public interface IXmlExporter
    {
        string FileFolderPath { get; }

        string FileName { get; }

        void Export();

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
