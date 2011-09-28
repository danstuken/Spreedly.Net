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
                                        CustomerId = TestConstants.TestCustomerId,
                                        ScreenName = TestConstants.TestCustomerId,
                                        Email = "email@domain.com"
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
            var custId = TestConstants.TestCustomerId;
            var sub = _subscribers.GetSubscriberByCustomerId(custId);

            Assert.AreEqual(custId, sub.Entity.CustomerId);
        }

        [Test]
        public void UpgradeSubscriberThenDowngradeToFreePlan_ReturnsDowngradedSubscriber()
        {
            var custId = TestConstants.TestCustomerId;
            var sub = _subscribers.GetSubscriberByCustomerId(custId);
            var subscriber = sub.Entity;
            Assert.AreEqual(custId, subscriber.CustomerId);

            var planFactory = _factory.GetSubscriptionPlanClient();
            var invoiceFactory = _factory.GetInvoiceClient();

            var plans = planFactory.GetSubscriptionPlans();
            var freePlan = plans.Entity.SubscriptionPlans.FirstOrDefault(p => p.Id.Value == TestConstants.FreePlanSubscriptionPlanId);
            var paidPlan = plans.Entity.SubscriptionPlans.FirstOrDefault(p => p.Id.Value == TestConstants.MediumSubscriptionPlanId);

            Assert.NotNull(freePlan);
            Assert.NotNull(paidPlan);

            var creditCard = new CreditCard
                    {
                        CardType = "visa",
                        ExpirationMonth = 12,
                        ExpirationYear = 2012,
                        FirstName = "Tester",
                        LastName = "Testing",
                        Number = TestConstants.ValidCard,
                        VerificationValue = "123"
                    };

            var payment = new Payment
                  {
                      AccountType = "credit-card",
                      CreditCard = creditCard
                  };
            
            _subscribers.SubscribeSubscriberToFreeTrial(subscriber.CustomerId, freePlan);

            var invoiceResponse = invoiceFactory.CreateInvoice(new Invoice
            {
                SubscriptionPlanId = paidPlan.Id,
                Subscriber = subscriber
            });

            var invoice = invoiceResponse.Entity;

            Assert.NotNull(invoice);

            invoiceFactory.PayInvoice(invoice, payment);

            _subscribers.AllowSubscriberAnotherFreeTrial(subscriber.CustomerId);

            _subscribers.SubscribeSubscriberToFreeTrial(sub.Entity.CustomerId, freePlan);
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