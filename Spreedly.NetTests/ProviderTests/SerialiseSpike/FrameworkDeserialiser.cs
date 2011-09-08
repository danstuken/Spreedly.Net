namespace Spreedly.NetTests.ProviderTests.SerialiseSpike
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using RestSharp;
    using RestSharp.Deserializers;

    public class FrameworkDeserialiser: IDeserializer
    {
        public T Deserialize<T>(RestResponse response) where T : new()
        {
            if (response == null || response.Content == null)
                return default(T);

            XmlDocument doc;
            return Deserialize<T>(XmlReader.Create(new StringReader(response.Content)));
        }

        private T Deserialize<T>(XmlReader contentReader)
        {
            var serializer = new XmlSerializer(typeof (T));
            return (T)serializer.Deserialize(contentReader);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
    }

}