namespace Spreedly.Net.Entities
{
    using RestSharp.Serializers;

    [SerializeAs(NameStyle = NameStyle.LowerCase)]
    public class Payment
    {
        [SerializeAs(Name = "account-type")]
        public string AccountType { get; set; }

        [SerializeAs(Name = "credit-card")]
        public CreditCard CreditCard { get; set; }
    }
}
