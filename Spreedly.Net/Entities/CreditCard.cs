namespace Spreedly.Net.Entities
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "credit-card")]
    public class CreditCard
    {
        [XmlElement(ElementName="number")]
        public string Number { get; set; }
        
        [XmlElement(ElementName = "verification-value")]
        public string VerificationValue { get; set; }

        [XmlElement(ElementName = "month")]
        public int ExpirationMonth { get; set; }
        
        [XmlElement(ElementName = "year")]
        public int ExpirationYear { get; set; }
        
        [XmlElement(ElementName = "first-name")]
        public string FirstName { get; set; }
        
        [XmlElement(ElementName = "last-name")]
        public string LastName { get; set; }
        
        [XmlElement(ElementName = "card-type")]
        public string CardType
        {
            get { return _cardType; }
            set
            {
                if (value != "visa" && value != "master" && value != "discover" && value != "american_express")
                    throw new ArgumentException("Expect card type of \"visa\", \"master\", \"discover\" or \"american_express\"");
                _cardType = value;
            }
        }
        private string _cardType;
    }
}