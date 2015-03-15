namespace XmlImporter
{
    using ProductsSystem.Data.Data;

    public interface IXmlImporter
    {
        void Import(IProductsSystemData data);
    }
}
