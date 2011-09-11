namespace Spreedly.Net.Entities
{
    using System.Xml.Serialization;
    
    [XmlRoot(ElementName="payment")]
    public class Payment
    {
        [XmlElement(ElementName = "account-type")]
        public string AccountType { get; set; }

        [XmlElement(ElementName = "credit-card")]
        public CreditCard CreditCard { get; set; }
    }
}
