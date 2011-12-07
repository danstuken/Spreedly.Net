namespace Spreedly.Entities
{
    using System.Xml.Serialization;

    using Spreedly.Xml;

    [XmlRoot(ElementName = "line-item")]
    public class LineItem
    {
        [XmlElement(ElementName = "amount")]
        public SerializableNullable<decimal> Amount { get; set; }

        [XmlElement(ElementName = "currency-code")]
        public string CurrencyCode { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "notes")]
        public string Notes { get; set; }

        [XmlElement(ElementName = "price")]
        public string Price { get; set; }

        #region ShouldSerialize Convention methods
        
        public bool ShouldSerializeAmount()
        {
            return Amount.HasValue;
        }

        #endregion
    }
}