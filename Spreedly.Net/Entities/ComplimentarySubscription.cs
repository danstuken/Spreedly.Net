namespace Spreedly.Entities
{
    using System;
    using System.Xml.Serialization;

    using Spreedly.Xml;

    [XmlRoot(ElementName="complimentary-subscription")]
    public class ComplimentarySubscription
    {
        [XmlElement(ElementName = "start-time")]
        public SerializableNullable<DateTime> StartTime { get; set; }

        [XmlElement(ElementName = "amount")]
        public SerializableNullable<decimal> Amount { get; set; }

        [XmlElement(ElementName = "duration-quantity")]
        public int DurationQuantity { get; set; }

        [XmlElement(ElementName = "duration-units")]
        public string DurationUnits { get; set; }

        [XmlElement(ElementName = "feature-level")]
        public string FeatureLevel { get; set; }

        #region Should Serialize Convention methods

        public bool ShouldSerializeStartTime()
        {
            return StartTime.HasValue;
        }

        public bool ShouldSerializeAmount()
        {
            return Amount.HasValue;
        }

        #endregion
    }
}