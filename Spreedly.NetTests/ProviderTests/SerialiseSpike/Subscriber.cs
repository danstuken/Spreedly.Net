namespace Spreedly.NetTests.ProviderTests.SerialiseSpike
{
    using System;
    using System.Xml.Serialization;
    using Spreedly.Net.Entities;

    [XmlRoot(ElementName = "subscriber")]
    public class Subscriber
    {

        [XmlElement(ElementName = "active", IsNullable = true)]
        public SerializableNullable<bool> Active { get; set; }

        [XmlElement(ElementName = "active-until", IsNullable = true)]
        public SerializableNullable<DateTime> ActiveUntil { get; set; }

        [XmlElement(ElementName = "billing-first-name")]
        public string BillingFirstName { get; set; }

        [XmlElement(ElementName = "billing-last-name")]
        public string BillingLastName { get; set; }

        [XmlElement(ElementName = "card-expires-before-next-auto-renew", IsNullable = true)]
        public SerializableNullable<bool> CardExpiresBeforeNextAutoRenew { get; set; }

        [XmlElement(ElementName = "created-at", IsNullable = true)]
        public SerializableNullable<DateTime> CreatedAt { get; set; }

        [XmlElement(ElementName = "customer-id")]
        public string CustomerId { get; set; }

        [XmlElement(ElementName = "eligible-for-free-trial", IsNullable = true)]
        public SerializableNullable<bool> EligibleForFreeTrial { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "feature-level")]
        public string FeatureLevel { get; set; }

        [XmlElement(ElementName = "lifetime-subscription", IsNullable = true)]
        public SerializableNullable<bool> LifetimeSubscription { get; set; }

        [XmlElement(ElementName = "on-trial", IsNullable = true)]
        public SerializableNullable<bool> OnTrial { get; set; }

        [XmlElement(ElementName = "on-gift", IsNullable = true)]
        public SerializableNullable<bool> OnGift { get; set; }

        [XmlElement(ElementName = "on-metered")]
        public SerializableNullable<bool> OnMetered { get; set; }

        [XmlElement(ElementName = "recurring", IsNullable = true)]
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

        [XmlElement(ElementName = "updated-at", IsNullable = true)]
        public SerializableNullable<DateTime> UpdatedAt { get; set; }

        [XmlElement(ElementName = "grace-until", IsNullable = true)]
        public SerializableNullable<DateTime> GraceUntil { get; set; }

        [XmlElement(ElementName = "in-grace-period", IsNullable = true)]
        public SerializableNullable<bool> InGracePeriod { get; set; }

        [XmlElement(ElementName = "ready-to-renew", IsNullable = true)]
        public SerializableNullable<bool> ReadyToRenew { get; set; }

        [XmlElement(ElementName = "ready-to-renew-since", IsNullable = true)]
        public SerializableNullable<DateTime> ReadyToRenewSince { get; set; }

        [XmlElement(ElementName = "payment-account-on-file", IsNullable = true)]
        public SerializableNullable<bool> PaymentAccountOnFile { get; set; }

        [XmlElement(ElementName = "payment-account-display", IsNullable = true)]
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
    }
}