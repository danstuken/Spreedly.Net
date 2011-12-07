namespace Spreedly.Entities
{
    using System;
    using System.Xml.Serialization;

    using Spreedly.Xml;

    [XmlRoot(ElementName = "subscription-plan")]
    public class SubscriptionPlan
    {
        [XmlElement(ElementName = "amount")]
        public SerializableNullable<decimal> Amount { get; set; }

        [XmlElement(ElementName = "charge-after-first-period")]
        public SerializableNullable<bool> ChargeAfterFirstPeriod { get; set; }

        [XmlElement(ElementName = "charge-later-duration-quantity")]
        public SerializableNullable<int> ChargeLaterDuration { get; set; }

        [XmlElement(ElementName = "charge-later-duration-units")]
        public string ChargerLaterDurationUnits { get; set; }

        [XmlElement(ElementName = "created-at")]
        public SerializableNullable<DateTime> CreatedAt { get; set; }

        [XmlElement(ElementName = "currency-code")]
        public string CurrencyCode { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "duration-quantity")]
        public SerializableNullable<int> Duration { get; set; }

        [XmlElement(ElementName = "duration-units")]
        public string DurationUnits { get; set; }

        [XmlElement(ElementName = "enabled")]
        public SerializableNullable<bool> Enabled { get; set; }

        [XmlElement(ElementName = "feature-level")]
        public string FeatureLevel { get; set; }

        [XmlElement(ElementName = "force-recurring")]
        public SerializableNullable<bool> ForceRecurring { get; set; }

        [XmlElement(ElementName = "id")]
        public SerializableNullable<int> Id { get; set; }

        [XmlElement(ElementName = "minimum-needed-for-charge")]
        public SerializableNullable<decimal> MinimumNeededForCharge { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "needs-to-be-renewed")]
        public SerializableNullable<bool> NeedsToBeRenewed { get; set; }

        [XmlElement(ElementName = "plan-type")]
        public string PlanType { get; set; }

        [XmlElement(ElementName = "return-url")]
        public string ReturnUrl { get; set; }

        [XmlElement(ElementName = "updated-at")]
        public SerializableNullable<DateTime> UpdatedAt { get; set; }

        [XmlElement(ElementName = "terms")]
        public string Terms { get; set; }

        [XmlElement(ElementName = "price")]
        public SerializableNullable<decimal> Price { get; set; }

        #region ShouldSerialize Convention methods

        public bool ShouldSerializeAmount()
        {
            return Amount.HasValue;
        }

        public bool ShouldSerializeChargeAfterFirstPeriod()
        {
            return ChargeAfterFirstPeriod.HasValue;
        }

        public bool ShouldSerializeChargeLaterDuration()
        {
            return ChargeLaterDuration.HasValue;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return CreatedAt.HasValue;
        }

        public bool ShouldSerializeDuration()
        {
            return Duration.HasValue;
        }

        public bool ShouldSerializeEnabled()
        {
            return Enabled.HasValue;
        }

        public bool ShouldSerializeForceRecurring()
        {
            return ForceRecurring.HasValue;
        }

        public bool ShouldSerializeId()
        {
            return Id.HasValue;
        }

        public bool ShouldSerializeMinimumNeededForCharge()
        {
            return MinimumNeededForCharge.HasValue;
        }

        public bool ShouldSerializeNeedsToBeRenewed()
        {
            return NeedsToBeRenewed.HasValue;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return UpdatedAt.HasValue;
        }

        public bool ShouldSerializePrice()
        {
            return Price.HasValue;
        }

        #endregion
    }
}