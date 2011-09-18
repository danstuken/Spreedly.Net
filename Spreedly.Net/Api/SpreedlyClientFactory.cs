namespace Spreedly.Net.Api
{
    using Client;

    public class SpreedlyClientFactory
    {
        private SpreedlyV4Api _spreedlyCaller;

        public SpreedlyClientFactory(ISpreedlyParameters parameters)
        {
            var client = new SpreedlyClient(parameters.ApiKey, "X", new SpreedlyRequestBuilder(parameters.ApiVersion, parameters.SiteName), new StatusResolver());
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