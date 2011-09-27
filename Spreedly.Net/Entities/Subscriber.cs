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
            return false;
        }

        public bool ShouldSerializeStoreCredit()
        {
            return false;
        }

        public bool ShouldSerializeRecurring()
        {
            return false;
        }

        public bool ShouldSerializeOnMetered()
        {
            return false;
        }

        public bool ShouldSerializeOnGift()
        {
            return false;
        }

        public bool ShouldSerializeOnTrial()
        {
            return false;
        }

        public bool ShouldSerializeLifetimeSubscription()
        {
            return false;
        }

        public bool ShouldSerializeEligibleForFreeTrial()
        {
            return false;
        }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        public bool ShouldSerializeCardExpiresBeforeNextAutoRenew()
        {
            return false;
        }

        public bool ShouldSerializeActiveUntil()
        {
            return false;
        }

        public bool ShouldSerializeActive()
        {
            return false;
        }

        public bool ShouldSerializeUpdatedAt()
        {
            return false;
        }

        public bool ShouldSerializeInGracePeriod()
        {
            return false;
        }

        public bool ShouldSerializeReadyToRenew()
        {
            return false;
        }

        public bool ShouldSerializeReadyToRenewSince()
        {
            return false;
        }

        public bool ShouldSerializePaymentAccountOnFile()
        {
            return false;
        }

        public bool ShouldSerializeToken()
        {
            return false;
        }

        public bool ShouldSerializeStoreCreditCurrencyCode()
        {
            return false;
        }

        public bool ShouldSerializeFeatureLevel()
        {
            return false;
        }

        public bool ShouldSerializeSubscriptionPlanName()
        {
            return false;
        }

        public bool ShouldSerializeBillingAddress1()
        {
            return false;
        }

        public bool ShouldSerializeBillingCity()
        {
            return false;
        }

        public bool ShouldSerializeBillingCountry()
        {
            return false;
        }

        public bool ShouldSerializeBillingPhoneNumber()
        {
            return false;
        }

        public bool ShouldSerializeBillingState()
        {
            return false;
        }

        public bool ShouldSerializeBillingZip()
        {
            return false;
        }

        public bool ShouldSerializePaymentAccountDisplay()
        {
            return false;
        }

        #endregion
    }
}