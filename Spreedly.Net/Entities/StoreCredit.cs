namespace Spreedly.Entities
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName="credit")]
    public class StoreCredit
    {
        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }
}