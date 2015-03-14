namespace TestXmlExportClass
{
    using System;
    using System.Xml.Serialization;

    using ProductsSystem.DataTransferObjects;

    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            WriteXml();
        }

        public static void WriteXml()
        {
            //var testSale = new SalesAggregated() { VendorName = "test vendor", Date = DateTime.Now, TotalSum = 23 };
            var overview = new Book("Serialization Overview");
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(SalesAggregated));

            var file = new System.IO.StreamWriter(
                @"c:\temp\SerializationOverview.xml");
            writer.Serialize(file, overview);
            file.Close();
        }


        public class Book
        {
            public Book()
            { 
            }

            public Book(string title)
            {
                this.Title = title;
            }

            [XmlAttribute]
            public string Title { get; set; }
        }
    }
}
