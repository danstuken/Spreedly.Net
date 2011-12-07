namespace Spreedly.Api
{
    using System;

    using Spreedly.Client;

    public class SpreedlyClientFactory
    {
        private SpreedlyV4Api _spreedlyCaller;

        public SpreedlyClientFactory(ISpreedlyParameters parameters)
            : this(parameters.SiteName, parameters.ApiKey)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreedlyClientFactory"/> class. 
        /// Creates a new SpreedlyClientFactory
        /// </summary>
        /// <param name="siteName">The Short site name from your Spreedly account settings</param>
        /// <param name="apiKey">The API Authentication Token from your Spreedly account settings</param>
        public SpreedlyClientFactory(string siteName, string apiKey)
        {
            var client = new SpreedlyClient(apiKey, "X", new SpreedlyRequestBuilder(siteName), new StatusResolver());
            _spreedlyCaller = new SpreedlyV4Api(client);
        }

        public T GetClient<T>()
        {
            if (typeof(T) == typeof(ISpreedlyInvoices))
                return (T)GetInvoiceClient();

            if (typeof(T) == typeof(ISpreedlySubscribers))
                return (T)GetSubscribersClient();

            if (typeof(T) == typeof(ISpreedlySubscriptionPlans))
                return (T)GetSubscriptionPlanClient();

            if (typeof(T) == typeof(ISpreedlyTest))
                return (T)GetTestClient();

            throw new InvalidOperationException(string.Format("Unsupported type {0}", typeof(T).Name));
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