﻿namespace Spreedly.Xml
{
    using System.IO;
    using System.Net;
    using System.Xml;
    using System.Xml.Serialization;

    using RestSharp;
    using RestSharp.Deserializers;

    internal class FrameworkDeserializer: IDeserializer
    {
        public T Deserialize<T>(RestResponse response) where T : new()
        {
            if (response == null || response.Content == null || (response.StatusCode != HttpStatusCode.Created && response.StatusCode != HttpStatusCode.OK))
                return default(T);

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