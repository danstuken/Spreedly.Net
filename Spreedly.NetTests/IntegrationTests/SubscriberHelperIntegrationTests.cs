namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Entities;
    using Net.Helpers;
    using Net.Helpers.Exceptions;

    public class SubscriberHelperIntegrationTests
    {
        [SetUp]
        public void Init()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };

            if (!subscriberHelper.Exists(subscriber.CustomerId))
                subscriberHelper.CreateSubscriber(subscriber.CustomerId, subscriber.Email, subscriber.ScreenName);
        }

        [Test]
        public void CreateSubscription_WithHelper_ReturnsInvoice()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
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

            var invoice = subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber, TestConstants.MediumSubscriptionPlanId, creditCard);

            Assert.NotNull(invoice);
        }

        [Test]
        public void UpgradeSubscription_ChargesProRatadAmount()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);

            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
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

            subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                       TestConstants.MediumSubscriptionPlanId,
                                                                       creditCard);
            var upgradeInvoice = subscriberHelper.ChangeSubscriberFeatureLevelWithOnFilePayment(subscriber, TestConstants.HighSubscriptionPlanId);

            Assert.AreEqual(15, upgradeInvoice.Amount.Value);
        }

        [Test]
        public void DowngradeSubscription_GivesStoreCreditToSubscriber()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
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

            subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber, TestConstants.HighSubscriptionPlanId, creditCard);
            subscriberHelper.ChangeSubscriberFeatureLevelWithOnFilePayment(subscriber, TestConstants.LowSubscriptionPlanId);
            var downgradedSubscriber = subscriberHelper.FetchSubscriber(subscriber.CustomerId);

            Assert.GreaterOrEqual(downgradedSubscriber.StoreCredit.Value, 0);
        }

        [Test]
        public void UpgradingFromFreeTrial_RetrunsSubscriptionInvoice()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
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

            var freeSubscriber = subscriberHelper.SubscribeToFreeTrialPlan(subscriber, TestConstants.FreePlanSubscriptionPlanId);
            Assert.NotNull(freeSubscriber);

            var invoice = subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(freeSubscriber,
                                                                                     TestConstants.MediumSubscriptionPlanId,
                                                                                     creditCard);
            Assert.NotNull(invoice);
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithHelper_UsingAnauthorisedCard_ThrowsForbiddenActionException()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
            var creditCard = new CreditCard
            {
                CardType = "visa",
                ExpirationMonth = 12,
                ExpirationYear = 2012,
                FirstName = "Tester",
                LastName = "Testing",
                Number = TestConstants.UnauthorisedCard,
                VerificationValue = "123"
            };

            var invoice = subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber, TestConstants.MediumSubscriptionPlanId, creditCard);
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithHelper_WhenGatewayTimesout_ThrowsPaymentGatewayTimeoutException()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
            var creditCard = new CreditCard
            {
                CardType = "visa",
                ExpirationMonth = 12,
                ExpirationYear = 2012,
                FirstName = "Tester",
                LastName = "Testing",
                Number = TestConstants.GatewayUnavailableCard,
                VerificationValue = "123"
            };

            var invoice = subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber, TestConstants.MediumSubscriptionPlanId, creditCard);
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithHelper_ForFreeSubscriptionPlan_ThrowsForbiddenActionException()
        {
            var subscriberHelper = new SubscriberHelper(TestConstants.TestSiteName, TestConstants.TestApiKey);
            var subscriber = new Subscriber
            {
                CustomerId = TestConstants.TestCustomerId,
                ScreenName = TestConstants.TestCustomerId,
                Email = "test@test.madeup"
            };
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

            var invoice = subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber, TestConstants.FreePlanSubscriptionPlanId, creditCard);
        }

        [TearDown]
        public void IntegrationTearDown()
        {
            var factory = new SpreedlyClientFactory(new SpreedlyParameters
            {
                ApiKey = TestConstants.TestApiKey,
                SiteName = TestConstants.TestSiteName
            });

            factory.GetTestClient().DeleteAllSubscribers();
        }
    }
}