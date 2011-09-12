namespace Spreedly.Net.Api
{
    using Client;

    public class SpreedlyClientFactory
    {
        private SpreedlyV4Api _spreedlyV4Client;

        public SpreedlyClientFactory(ISpreedlyParameters parameters)
        {
            var client = new SpreedlyClient(parameters.ApiKey, "X", new SpreedlyRequestBuilder(parameters.ApiVersion, parameters.SiteName), new StatusResolver());
            _spreedlyV4Client = new SpreedlyV4Api(client);
        }

        public ISpreedlyInvoices GetInvoiceClient()
        {
            return _spreedlyV4Client;
        }

        public ISpreedlySubscribers GetSubscribersClient()
        {
            return _spreedlyV4Client;
        }

        public ISpreedlySubscriptionPlans GetSubscriptionPlanClient()
        {
            return _spreedlyV4Client;
        }

        public ISpreedlyTest GetTestClient()
        {
            return _spreedlyV4Client;
        }
    }
}