namespace BasicSerialization
{
    using System.IO;
    using System.Xml.Serialization;
    using Models;

    public class Serializator
    {
        public Catalog ReadXml(string fullFilePath, string xmlNamespace)
        {
            Catalog result;

            var serializer = new XmlSerializer(typeof(Catalog), xmlNamespace);
            using (var fs = new FileStream(fullFilePath, FileMode.OpenOrCreate))
            {
                result = (Catalog)serializer.Deserialize(fs);
            }

            return result;
        }

        public void WriteXml(Catalog catalog, string fullFilePath, string xmlNamespace)
        {
            var serializer = new XmlSerializer(typeof(Catalog), xmlNamespace);
            using (var fs = new FileStream(fullFilePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, catalog);
            }
        }
    }
}
