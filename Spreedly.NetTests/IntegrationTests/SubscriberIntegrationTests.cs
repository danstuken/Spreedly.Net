namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;

    public class SubscriberIntegrationTests
    {
        private string _apiKey = "61a2304391b862d526e95f11ee7a3815f0857e3f";
        private string _apiVersion = "v4";
        private string _siteName = "sitedocdan-test";

        private ISpreedlySubscribers _subscribers;

        [SetUp]
        public void init()
        {
            _subscribers =
                new SpreedlySubscriberClient(new SpreedlyClient(_apiKey, "X",
                                                                new SpreedlyRequestBuilder(_apiVersion, _siteName),
                                                                new StatusResolver()));
        }

        [Test]
        public void ListSubscribersForSite_ReturnsStatusOK()
        {
            var subs = _subscribers.GetSubscribers();

            Assert.AreEqual(SpreedlyStatus.Ok, subs.Status);
        }

        [Test]
        public void RetrieveSubscriberUsingCustomerId_ReturnsSubscriber()
        {
            var custId = "aef789956af61024d82ec270039601b91e06262a";
            var sub = _subscribers.GetSubscriberByCustomerId(custId);

            Assert.AreEqual(SpreedlyStatus.Ok, sub.Status);
            Assert.AreEqual(custId, sub.Entity.CustomerId);
        }

        [Test]
        public void RetrieveSubscriberUsingMadeUpCustomerId_ReturnsStatus404()
        {
            var custId = "blahblahblah";
            var sub = _subscribers.GetSubscriberByCustomerId(custId);

            Assert.AreEqual(SpreedlyStatus.NotFound, sub.Status);
        }
    }
}