namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;

    using Spreedly.Api;
    using Spreedly.Client;

    [TestFixture]
    public class SubscriptionPlansIntegrationTests
    {
        private SpreedlyClientFactory _factory;
        private ISpreedlySubscriptionPlans _subscriptionPlans;

        [TestFixtureSetUp]
        public void init()
        {
            _factory = new SpreedlyClientFactory(new SpreedlyParameters
            {
                ApiKey = TestConstants.TestApiKey,
                SiteName = TestConstants.TestSiteName
            });

            _subscriptionPlans = _factory.GetSubscriptionPlanClient();
        }

        [Test]
        public void GetAvailableSubscriptionPlans_ReturnsStatusOk()
        {
            var plans = _subscriptionPlans.GetSubscriptionPlans();
            
            Assert.AreEqual(SpreedlyStatus.Ok, plans.Status);
        }

        [Test]
        public void GetAvailableSubscriptionPlans_ReturnsSomePlans()
        {
            var plans = _subscriptionPlans.GetSubscriptionPlans();

            Assert.GreaterOrEqual(plans.Entity.SubscriptionPlans.Count, 1);
        }
    }
}