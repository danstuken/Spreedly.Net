namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;

    public class InvoiceIntegrationTests
    {
        private string _apiKey = "61a2304391b862d526e95f11ee7a3815f0857e3f";
        private string _apiVersion = "v4";
        private string _siteName = "sitedocdan-test";

        private ISpreedlySubscribers _subscribers;

        [TestFixtureSetUp]
        public void init()
        {
            SpreedlyClientFactory factory = new SpreedlyClientFactory(new SpreedlyParameters
                                                                          {
                                                                              ApiKey = _apiKey,
                                                                              ApiVersion = _apiVersion,
                                                                              SiteName = _siteName
                                                                          });
            _subscribers = factory.GetSubscribersClient();

            var newSubscriber = new Subscriber
                                    {
                                        CustomerId = "TestCustomerId",
                                        ScreenName = "TestCustomerId"
                                    };
            _subscribers.CreateSubscriber(newSubscriber);
        }

        [Test]
        public void CreateInvoiceForUser_ReturnsInvoice()
        {
            
        }

        [TestFixtureTearDown]
        public void IntegrationTearDown()
        {
            _subscribers.DeleteAllSubscribers();
        }
    }
}