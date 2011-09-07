namespace Spreedly.Net.Entities
{
    using System;
    using RestSharp.Serializers;

    [SerializeAs(NameStyle = NameStyle.LowerCase)]
    public class SubscriptionPlan
    {
        public Decimal? Amount { get; set; }
        [SerializeAs(Name = "charge-after-first-period")]
        public bool? ChargeAfterFirstPeriod { get; set; }
        [SerializeAs(Name = "charge-later-duration-quantity")]
        public int? ChargeLaterDuration { get; set; }
        [SerializeAs(Name = "charge-later-duration-units")]
        public string ChargerLaterDurationUnits { get; set; }
        [SerializeAs(Name = "created-at")]
        public DateTime? CreatedAt { get; set; }
        [SerializeAs(Name = "currency-code")]
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        [SerializeAs(Name = "duration-quantity")]
        public int? Duration { get; set; }
        [SerializeAs(Name = "duration-units")]
        public string DurationUnits { get; set; }
        public bool? Enabled { get; set; }
        [SerializeAs(Name = "feature-level")]
        public string FeatureLevel { get; set; }
        [SerializeAs(Name = "force-recurring")]
        public bool? ForceRecurring { get; set; }
        public int? Id { get; set; }
        [SerializeAs(Name = "minimum-needed-for-charge")]
        public decimal? MinimumNeededForCharge { get; set; }
        public string Name { get; set; }
        [SerializeAs(Name = "needs-to-be-renewed")]
        public bool? NeedsToBeRenewed { get; set; }
        [SerializeAs(Name = "plan-type")]
        public string PlanType { get; set; }
        [SerializeAs(Name = "return-url")]
        public string ReturnUrl { get; set; }
        [SerializeAs(Name = "updated-at")]
        public DateTime? UpdatedAt { get; set; }
        public string Terms { get; set; }
        public decimal? Price { get; set; }
    }
}