namespace Spreedly.Net.Entities
{
    using System;
    using System.Xml.Serialization;
    using Xml;

    [XmlRoot(ElementName = "invoice")]
    public class Invoice
    {
        [XmlElement(ElementName = "closed")]
        public SerializableNullable<bool> Closed { get; set; }

        [XmlElement(ElementName = "created-at")]
        public SerializableNullable<DateTime> CreatedAt { get; set; }

        [XmlElement(ElementName = "response-client-message")]
        public string ResponseClientMessage { get; set; }
        
        [XmlElement(ElementName = "response-customer-message")]
        public string ResponseCustomerMessage { get; set; }
        
        [XmlElement(ElementName = "response-message")]
        public string ResponseMessage { get; set; }
        
        [XmlElement(ElementName = "token")]
        public string Token { get; set; }
        
        [XmlElement(ElementName = "updated-at")]
        public SerializableNullable<DateTime> UpdatedAt { get; set; }

        [XmlElement(ElementName = "price")]
        public string Price { get; set; }
        
        [XmlElement(ElementName = "amount")]
        public SerializableNullable<decimal> Amount { get; set; }

        [XmlElement(ElementName = "currency-code")]
        public string CurrencyCode { get; set; }
        
        [XmlElement(ElementName = "subscriber")]
        public Subscriber Subscriber { get; set; }
        
        [XmlElement(ElementName = "subscription-plan-id")]
        public SerializableNullable<int> SubscriptionPlanId { get; set; }

        [XmlElement(ElementName = "line-items")]
        public LineItemList LineItemList { get; set; }

        #region ShouldSerialize Convention methods

        public bool ShouldSerializeClosed()
        {
            return Closed.HasValue;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return CreatedAt.HasValue;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return UpdatedAt.HasValue;
        }

        public bool ShouldSerializeAmount()
        {
            return Amount.HasValue;
        }

        public bool ShouldSerializeSubscriptionPlanId()
        {
            return SubscriptionPlanId.HasValue;
        }

        #endregion
    }
}