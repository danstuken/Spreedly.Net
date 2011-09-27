namespace Spreedly.NetTests.IntegrationTests
{
    using System.Linq;
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;

    public class SubscriberIntegrationTests
    {
        private ISpreedlySubscribers _subscribers;
        private SpreedlyClientFactory _factory;

        [TestFixtureSetUp]
        public void init()
        {
            _factory = new SpreedlyClientFactory(new SpreedlyParameters
                                                    {
                                                        ApiKey = TestConstants.TestApiKey,
                                                        SiteName = TestConstants.TestSiteName
                                                    });

            _subscribers = _factory.GetSubscribersClient();

            var newSubscriber = new Subscriber
                                    {
                                        CustomerId = "TestCustomerId",
                                        ScreenName = "TestCustomerId"
                                    };
            _subscribers.CreateSubscriber(newSubscriber);
        }

        [Test]
        public void ListSubscribersForSite_ReturnsStatusOK()
        {
            var subs = _subscribers.GetSubscribers();

            Assert.AreEqual(SpreedlyStatus.Ok, subs.Status);
            Assert.True(subs.Entity.Subscribers.Count > 0);
        }

        [Test]
        public void RetrieveSubscriberUsingCustomerId_ReturnsSubscriber()
        {
            var custId = "TestCustomerId";
            var sub = _subscribers.GetSubscriberByCustomerId(custId);

            Assert.AreEqual(custId, sub.Entity.CustomerId);
        }

        [Test]
        public void RetrieveSubscriberUsingMadeUpCustomerId_ReturnsStatus404()
        {
            var custId = "blahblahblah";
            var sub = _subscribers.GetSubscriberByCustomerId(custId);

            Assert.AreEqual(SpreedlyStatus.NotFound, sub.Status);
        }

        [TestFixtureTearDown]
        public void IntegrationTearDown()
        {
            _factory.GetTestClient().DeleteAllSubscribers();
        }
    }
}