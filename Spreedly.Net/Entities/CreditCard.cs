namespace Spreedly.Net.Entities
{
    using System;
    using RestSharp.Serializers;

    [SerializeAs(NameStyle = NameStyle.LowerCase)]
    public class CreditCard
    {
        public string Number { get; set; }
        
        [SerializeAs(Name = "verification-value")]
        public string VerificationValue { get; set; }

        [SerializeAs(Name = "month")]
        public int ExpirationMonth { get; set; }
        
        [SerializeAs(Name = "year")]
        public int ExpirationYear { get; set; }
        
        [SerializeAs(Name = "first-name")]
        public string FirstName { get; set; }
        
        [SerializeAs(Name = "last-name")]
        public string LastName { get; set; }
        
        [SerializeAs(Name = "card-type")]
        public string CardType
        {
            get { return _cardType; }
            set
            {
                if (value != "visa" && value != "master" && value != "discover" && value != "american_express")
                    throw new ArgumentException("Expect card type of \"visa\", \"master\", \"discover\" or \"american_express\"");
                _cardType = value;
            }
        }
        private string _cardType;
    }
}