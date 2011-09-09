namespace Spreedly.Net.Entities
{
    using System;
    using System.Xml.Serialization;
    using Xml;

    [XmlRoot(ElementName = "subscriber")]
    public class Subscriber
    {
        [XmlElement(ElementName = "active")]
        public SerializableNullable<bool> Active { get; set; }

        [XmlElement(ElementName = "active-until")]
        public SerializableNullable<DateTime> ActiveUntil { get; set; }

        [XmlElement(ElementName = "billing-first-name")]
        public string BillingFirstName { get; set; }

        [XmlElement(ElementName = "billing-last-name")]
        public string BillingLastName { get; set; }

        [XmlElement(ElementName = "card-expires-before-next-auto-renew")]
        public SerializableNullable<bool> CardExpiresBeforeNextAutoRenew { get; set; }

        [XmlElement(ElementName = "created-at")]
        public SerializableNullable<DateTime> CreatedAt { get; set; }

        [XmlElement(ElementName = "customer-id")]
        public string CustomerId { get; set; }

        [XmlElement(ElementName = "eligible-for-free-trial")]
        public SerializableNullable<bool> EligibleForFreeTrial { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "feature-level")]
        public string FeatureLevel { get; set; }

        [XmlElement(ElementName = "lifetime-subscription")]
        public SerializableNullable<bool> LifetimeSubscription { get; set; }

        [XmlElement(ElementName = "on-trial")]
        public SerializableNullable<bool> OnTrial { get; set; }

        [XmlElement(ElementName = "on-gift")]
        public SerializableNullable<bool> OnGift { get; set; }

        [XmlElement(ElementName = "on-metered")]
        public SerializableNullable<bool> OnMetered { get; set; }

        [XmlElement(ElementName = "recurring")]
        public SerializableNullable<bool> Recurring { get; set; }

        [XmlElement(ElementName = "screen-name")]
        public string ScreenName { get; set; }

        [XmlElement(ElementName = "store-credit")]
        public SerializableNullable<decimal> StoreCredit { get; set; }

        [XmlElement(ElementName = "store-credit-currency-code")]
        public string StoreCreditCurrencyCode { get; set; }

        [XmlElement(ElementName = "subscription-plan-name")]
        public string SubscriptionPlanName { get; set; }

        [XmlElement(ElementName = "token")]
        public string Token { get; set; }

        [XmlElement(ElementName = "updated-at")]
        public SerializableNullable<DateTime> UpdatedAt { get; set; }
        
        [XmlElement(ElementName = "grace-until")]
        public SerializableNullable<DateTime> GraceUntil { get; set; }

        [XmlElement(ElementName = "in-grace-period")]
        public SerializableNullable<bool> InGracePeriod { get; set; }

        [XmlElement(ElementName = "ready-to-renew")]
        public SerializableNullable<bool> ReadyToRenew { get; set; }

        [XmlElement(ElementName = "ready-to-renew-since")]
        public SerializableNullable<DateTime> ReadyToRenewSince { get; set; }

        [XmlElement(ElementName = "payment-account-on-file")]
        public SerializableNullable<bool> PaymentAccountOnFile { get; set; }

        [XmlElement(ElementName = "payment-account-display")]
        public string PaymentAccountDisplay { get; set; }

        [XmlElement(ElementName = "billing-address1")]
        public string BillingAddress1 { get; set; }

        [XmlElement(ElementName = "billing-city")]
        public string BillingCity { get; set; }

        [XmlElement(ElementName = "billing-state")]
        public string BillingState { get; set; }

        [XmlElement(ElementName = "billing-zip")]
        public string BillingZip { get; set; }

        [XmlElement(ElementName = "billing-country")]
        public string BillingCountry { get; set; }

        [XmlElement(ElementName = "billing-phone-number")]
        public string BillingPhoneNumber { get; set; }

        [XmlElement(ElementName = "payment-method")]
        public CreditCard PaymentMethod { get; set; }

        #region ShouldSerialize Convention Methods
        
        public bool ShouldSerializeGraceUntil()
        {
            return GraceUntil.HasValue;
        }

        public bool ShouldSerializeStoreCredit()
        {
            return StoreCredit.HasValue;
        }

        public bool ShouldSerializeRecurring()
        {
            return Recurring.HasValue;
        }

        public bool ShouldSerializeOnMetered()
        {
            return OnMetered.HasValue;
        }

        public bool ShouldSerializeOnGift()
        {
            return OnGift.HasValue;
        }

        public bool ShouldSerializeOnTrial()
        {
            return OnTrial.HasValue;
        }

        public bool ShouldSerializeLifetimeSubscription()
        {
            return LifetimeSubscription.HasValue;
        }

        public bool ShouldSerializeEligibleForFreeTrial()
        {
            return EligibleForFreeTrial.HasValue;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return CreatedAt.HasValue;
        }

        public bool ShouldSerializeCardExpiresBeforeNextAutoRenew()
        {
            return CardExpiresBeforeNextAutoRenew.HasValue;
        }

        public bool ShouldSerializeActiveUntil()
        {
            return ActiveUntil.HasValue;
        }

        public bool ShouldSerializeActive()
        {
            return Active.HasValue;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return UpdatedAt.HasValue;
        }

        public bool ShouldSerializeInGracePeriod()
        {
            return InGracePeriod.HasValue;
        }

        public bool ShouldSerializeReadyToRenew()
        {
            return ReadyToRenew.HasValue;
        }

        public bool ShouldSerializeReadyToRenewSince()
        {
            return ReadyToRenewSince.HasValue;
        }

        public bool ShouldSerializePaymentAccountOnFile()
        {
            return PaymentAccountOnFile.HasValue;
        }

        #endregion
    }
}