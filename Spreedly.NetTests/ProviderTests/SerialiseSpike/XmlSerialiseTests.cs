namespace Spreedly.NetTests.ProviderTests.SerialiseSpike
{
    using System.Text;
    using NUnit.Framework;
    using RestSharp;

    //Really shonky tests just to make sure we're going in the right direction.

    public class XmlSerialiseTests
    {
        private string subscriberXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<subscriber>
  <active-until type=""datetime"" nil=""true""></active-until>
  <grace-until type=""datetime"" nil=""true""></grace-until>
  <ready-to-renew-since type=""datetime"" nil=""true""></ready-to-renew-since>
  <billing-first-name nil=""true""></billing-first-name>
  <billing-last-name nil=""true""></billing-last-name>
  <created-at type=""datetime"">2011-09-06T16:59:34Z</created-at>
  <customer-id>aef789956af61024d82ec270039601b91e06262a</customer-id>
  <eligible-for-free-trial type=""boolean"">true</eligible-for-free-trial>
  <email nil=""true""></email>
  <lifetime-subscription type=""boolean"">false</lifetime-subscription>
  <on-gift type=""boolean"" nil=""true""></on-gift>
  <on-metered type=""boolean"">false</on-metered>
  <on-trial type=""boolean"">false</on-trial>
  <ready-to-renew type=""boolean"">false</ready-to-renew>
  <recurring type=""boolean"" nil=""true""></recurring>
  <screen-name nil=""true""></screen-name>
  <store-credit type=""decimal"">0.0</store-credit>
  <store-credit-currency-code>USD</store-credit-currency-code>
  <token>771e8afeb012146b29ab4a5e66723f129f1362ca</token>
  <updated-at type=""datetime"">2011-09-06T16:59:34Z</updated-at>
  <card-expires-before-next-auto-renew type=""boolean"">false</card-expires-before-next-auto-renew>
  <subscription-plan-name></subscription-plan-name>
  <active type=""boolean"">false</active>
  <in-grace-period type=""boolean"">false</in-grace-period>
  <feature-level type=""string""></feature-level>
  <payment-account-on-file type=""boolean"">false</payment-account-on-file>
  <payment-account-display type=""string""></payment-account-display>
  <billing-address1 type=""string""></billing-address1>
  <billing-city type=""string""></billing-city>
  <billing-state type=""string""></billing-state>
  <billing-zip type=""string""></billing-zip>
  <billing-country type=""string""></billing-country>
  <billing-phone-number type=""string""></billing-phone-number>
  <invoices type=""array""/>
</subscriber>
";

        [Test]
        public void SubscriberDeserialises_WithoutException()
        {
            var deserialiser = new FrameworkDeserialiser();
            var xmlResponse = new RestResponse();
            xmlResponse.Content = subscriberXml;

            var subscriber = deserialiser.Deserialize<Subscriber>(xmlResponse);

            Assert.NotNull(subscriber);
        }

        [Test]
        public void SubscriberSerializes_ToSpreedlyFormat()
        {
            var deserialiser = new FrameworkDeserialiser();
            var xmlResponse = new RestResponse();
            xmlResponse.Content = subscriberXml;
            var subscriber = deserialiser.Deserialize<Subscriber>(xmlResponse);

            var serialiser = new FrameworkSerializer();
            serialiser.Encoding = Encoding.UTF8;
            var serialisedXml = serialiser.Serialize(subscriber);

            Assert.True(serialisedXml.Contains(@"<active-until nil=""true"""));
            Assert.True(serialisedXml.Contains(@"<recurring nil=""true"""));
            Assert.True(serialisedXml.Contains(@"<updated-at>2011-09-06T17:59:34+01:00"));
        }
    }
}