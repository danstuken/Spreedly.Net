namespace Spreedly.Entities
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "fee")]
    public class Fee
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "group")]
        public string Group { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }
}