namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;

    using Net.Helpers;

    using Spreedly.Api;
    using Spreedly.Client;
    using Spreedly.Entities;

    public class InvoiceIntegrationTests
    {
        private ISpreedlySubscribers _subscribers;
        private ISpreedlyInvoices _invoices;
        private SpreedlyClientFactory _factory;

        [SetUp]
        public void init()
        {
            _factory = new SpreedlyClientFactory(new SpreedlyParameters
                                                     {
                                                         ApiKey = TestConstants.TestApiKey,
                                                         SiteName = TestConstants.TestSiteName
                                                     });
            _subscribers = _factory.GetSubscribersClient();
            _invoices = _factory.GetInvoiceClient();

            var newSubscriber = new Subscriber
                                    {
                                        CustomerId = TestConstants.TestCustomerId,
                                        ScreenName = TestConstants.TestCustomerId,
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
                                                                               CustomerId = TestConstants.TestCustomerId,
                                                                               ScreenName = TestConstants.TestCustomerId,
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
                                                       Number = TestConstants.ValidCard,
                                                       VerificationValue = "123"
                                                   }
                              };

            var paymentResult = _invoices.PayInvoice(invoice.Entity, payment);

            Assert.AreEqual(SpreedlyStatus.Ok, paymentResult.Status);

        }

        [Test]
        public void CreateInvoiceForUser_WithUnauthorisedCard_ReturnsForbiddenStatus()
        {
            var invoice = _invoices.CreateInvoice(new Invoice
            {
                SubscriptionPlanId = 14143,
                Subscriber = new Subscriber
                {
                    CustomerId = TestConstants.TestCustomerId,
                    ScreenName = TestConstants.TestCustomerId,
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
                    Number = TestConstants.UnauthorisedCard,
                    VerificationValue = "123"
                }
            };

            var paymentResult = _invoices.PayInvoice(invoice.Entity, payment);

            Assert.AreEqual(SpreedlyStatus.Forbidden, paymentResult.Status);
        }

        [TearDown]
        public void IntegrationTearDown()
        {
            _factory.GetTestClient().DeleteAllSubscribers();
        }
    }
}