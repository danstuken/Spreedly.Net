namespace Spreedly.NetTests.IntegrationTests
{
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;

    [TestFixture]
    public class SubscriptionPlansIntegrationTests
    {
        private string _apiKey = "61a2304391b862d526e95f11ee7a3815f0857e3f";
        private string _siteName = "sitedocdan-test";

        private SpreedlyClientFactory _factory;
        private ISpreedlySubscriptionPlans _subscriptionPlans;

        [TestFixtureSetUp]
        public void init()
        {
            _factory = new SpreedlyClientFactory(new SpreedlyParameters
            {
                ApiKey = _apiKey,
                SiteName = _siteName
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