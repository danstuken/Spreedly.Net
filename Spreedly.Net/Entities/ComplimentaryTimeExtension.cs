namespace Spreedly.Net.Entities
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "complimentary-time-extension")]
    public class ComplimentaryTimeExtension
    {
        [XmlElement(ElementName="duration-quantity")]
        public int DurationQuantity { get; set; }

        [XmlElement(ElementName = "duration-units")]
        public string DurationUnits { get; set; }
    }
}