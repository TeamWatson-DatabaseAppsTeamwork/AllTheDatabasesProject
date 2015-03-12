namespace JsonExporter
{
    using System.Web.Script.Serialization;

    public class JsonExporter
    {
        public static string ToJson(object obj)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
    }
}
