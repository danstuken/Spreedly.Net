namespace Spreedly.Net.Api
{
    using Client;

    public class SpreedlyClientFactory
    {
        private SpreedlyV4Api _spreedlyCaller;

        public SpreedlyClientFactory(ISpreedlyParameters parameters): this(parameters.ApiKey, parameters.SiteName)
        {
        }

        public SpreedlyClientFactory(string apiKey, string siteName)
        {
            var client = new SpreedlyClient(apiKey, "X", new SpreedlyRequestBuilder(siteName), new StatusResolver());
            _spreedlyCaller = new SpreedlyV4Api(client);
        }

        public ISpreedlyInvoices GetInvoiceClient()
        {
            return _spreedlyCaller;
        }

        public ISpreedlySubscribers GetSubscribersClient()
        {
            return _spreedlyCaller;
        }

        public ISpreedlySubscriptionPlans GetSubscriptionPlanClient()
        {
            return _spreedlyCaller;
        }

        public ISpreedlyTest GetTestClient()
        {
            return _spreedlyCaller;
        }
    }
}