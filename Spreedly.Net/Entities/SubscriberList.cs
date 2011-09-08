namespace Spreedly.Net.Entities
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    
    [XmlRoot(ElementName = "subscribers")]
    public class SubscriberList
    {
        [XmlElement(ElementName = "subscriber")]
        public List<Subscriber> Subscribers { get; set; }

        public SubscriberList()
        {
            Subscribers = new List<Subscriber>();
        }
    }
}