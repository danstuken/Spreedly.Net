namespace Spreedly.Net.Entities
{
    using RestSharp.Serializers;

    [SerializeAs(NameStyle = NameStyle.LowerCase)]
    public class Invoice
    {
        [SerializeAs(Name = "subscription-plan-id")]
        public int SubscriptionPlanId { get; set; }



        public Subscriber Subscriber { get; set; }
    }
}