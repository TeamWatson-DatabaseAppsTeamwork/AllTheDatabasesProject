namespace XmlImporter
{
    public interface IXmlImporter
    {
        void Import();

        void SetDefaultFileFolder(string fileFolderPath);

        void SetFileName(string fileName);
    }
}
