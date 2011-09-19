namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;

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