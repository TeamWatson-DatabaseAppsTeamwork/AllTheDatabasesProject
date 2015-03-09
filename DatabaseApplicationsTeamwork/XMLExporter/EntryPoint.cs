using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLExporter
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            WriteXml();
        }

        public static void WriteXml()
        {
            var overview = new Book("Serialization Overview");
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(Book));

            var file = new System.IO.StreamWriter(
                @"c:\temp\SerializationOverview.xml");
            writer.Serialize(file, overview);
            file.Close();
        }


        public class Book
        {
            public Book(string title)
            {
                this.Title = title;
            }

            public string Title { get; set; }
        }
    }
}
