namespace Spreedly.Net.Entities
{
    using System;
    using RestSharp.Serializers;

    [SerializeAs(NameStyle = NameStyle.LowerCase)]
    public class Subscriber
    {

        public bool? Active { get; set; }
        [SerializeAs(Name = "active-until")]
        public DateTime? ActiveUntil { get; set; }
        [SerializeAs(Name = "billing-first-name")]
        public string BillingFirstName { get; set; }
        [SerializeAs(Name = "billing-last-name")]
        public string BillingLastName { get; set; }
        [SerializeAs(Name = "card-expires-before-next-auto-renew")]
        public bool? CardExpiresBeforeNextAutoRenew { get; set; }
        [SerializeAs(Name = "created-at")]
        public DateTime? CreatedAt { get; set; }
        [SerializeAs(Name = "customer-id")]
        public string CustomerId { get; set; }
        [SerializeAs(Name = "eligible-for-free-trial")]
        public bool? EligibleForFreeTrial { get; set; }
        public string Email { get; set; }
        [SerializeAs(Name = "feature-level")]
        public string FeatureLevel { get; set; }
        [SerializeAs(Name = "lifetime-subscription")]
        public bool? LifetimeSubscription { get; set; }
        [SerializeAs(Name = "on-trial")]
        public bool? OnTrial { get; set; }
        [SerializeAs(Name = "on-gift")]
        public bool? OnGift { get; set; }
        [SerializeAs(Name = "on-metered")]
        public bool? OnMetered { get; set; }
        public bool? Recurring { get; set; }
        [SerializeAs(Name = "screen-name")]
        public string ScreenName { get; set; }
        [SerializeAs(Name = "store-credit")]
        public decimal? StoreCredit { get; set; }
        [SerializeAs(Name = "store-credit-currency-code")]
        public string StoreCreditCurrencyCode { get; set; }
        [SerializeAs(Name = "subscription-plan-name")]
        public string SubscriptionPlanName { get; set; }
        public string Token { get; set; }
        [SerializeAs(Name = "updated-at")]
        public DateTime? UpdatedAt { get; set; }
        [SerializeAs(Name = "grace-until")]
        public DateTime? GraceUntil { get; set; }
        [SerializeAs(Name = "in-grace-period")]
        public bool? InGracePeriod { get; set; }
        [SerializeAs(Name = "ready-to-renew")]
        public bool? ReadyToRenew { get; set; }
        [SerializeAs(Name = "ready-to-renew-since")]
        public DateTime? ReadyToRenewSince { get; set; }
        [SerializeAs(Name = "payment-account-on-file")]
        public bool? PaymentAccountOnFile { get; set; }
        [SerializeAs(Name = "payment-account-display")]
        public bool? PaymentAccountDisplay { get; set; }
        [SerializeAs(Name = "billing-address1")]
        public string BillingAddress1 { get; set; }
        [SerializeAs(Name = "billing-city")]
        public string BillingCity { get; set; }
        [SerializeAs(Name = "billing-state")]
        public string BillingState { get; set; }
        [SerializeAs(Name = "billing-zip")]
        public string BillingZip { get; set; }
        [SerializeAs(Name = "billing-country")]
        public string BillingCountry { get; set; }
        [SerializeAs(Name = "billing-phone-number")]
        public string BillingPhoneNumber { get; set; }
        [SerializeAs(Name = "payment-method")]
        public CreditCard PaymentMethod { get; set; }
    }
}