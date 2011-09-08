namespace Spreedly.Net.Xml
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using RestSharp.Serializers;

    public class FrameworkSerializer: ISerializer
    {
        public string Serialize(object obj)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, this.Namespace);
            var serializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
            var writer = new EncodingStringWriter(this.Encoding);
            serializer.Serialize(writer, obj, ns);

            return writer.ToString();
        }

        public Encoding Encoding { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }


        private class EncodingStringWriter : StringWriter
        {
            private readonly Encoding encoding;

            public EncodingStringWriter(Encoding encoding)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get
                {
                    return this.encoding;
                }
            }
        }
    }
}