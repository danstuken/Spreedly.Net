namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;

    public class InvoiceIntegrationTests
    {
        private string _apiKey = "61a2304391b862d526e95f11ee7a3815f0857e3f";
        private string _siteName = "sitedocdan-test";

        private ISpreedlySubscribers _subscribers;
        private ISpreedlyInvoices _invoices;
        private SpreedlyClientFactory _factory;

        [TestFixtureSetUp]
        public void init()
        {
            _factory = new SpreedlyClientFactory(new SpreedlyParameters
                                                     {
                                                         ApiKey = _apiKey,
                                                         SiteName = _siteName
                                                     });
            _subscribers = _factory.GetSubscribersClient();
            _invoices = _factory.GetInvoiceClient();

            var newSubscriber = new Subscriber
                                    {
                                        CustomerId = "TestCustomerId",
                                        ScreenName = "TestCustomerId",
                                        Email = "test@test.madeup"
                                    };
            _subscribers.CreateSubscriber(newSubscriber);
        }

        [Test]
        public void CreateInvoiceForUser_ReturnsInvoice()
        {
            var invoice = _invoices.CreateInvoice(new Invoice
                                                      {
                                                          SubscriptionPlanId = 14143,
                                                          Subscriber = new Subscriber
                                                                           {
                                                                               CustomerId = "TestCustomerId",
                                                                               ScreenName = "TestCustomerId",
                                                                               Email = "test@test.madeup"
                                                                           }
                                                      });
            
            Assert.AreEqual(SpreedlyStatus.Created, invoice.Status);

            var payment = new Payment
                              {
                                  AccountType = "credit-card",
                                  CreditCard = new CreditCard
                                                   {
                                                       CardType = "visa",
                                                       ExpirationMonth = 12,
                                                       ExpirationYear = 2012,
                                                       FirstName = "Tester",
                                                       LastName = "Testing",
                                                       Number = "4222222222222",
                                                       VerificationValue = "123"
                                                   }
                              };

            var paymentResult = _invoices.PayInvoice(invoice.Entity, payment);

            Assert.AreEqual(SpreedlyStatus.Ok, paymentResult.Status);

        }

        [TestFixtureTearDown]
        public void IntegrationTearDown()
        {
            _factory.GetTestClient().DeleteAllSubscribers();
        }
    }
}