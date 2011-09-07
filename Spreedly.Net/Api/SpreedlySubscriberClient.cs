namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public class SpreedlySubscriberClient: ISpreedlySubscribers
    {
        private ISpreedlyParameters _parameters;
        private IRequestBuilder _requestBuilder;
        private IStatusResolver _statusResolver;

        public SpreedlySubscriberClient(ISpreedlyParameters parameters)
        {
            _parameters = parameters;
            _requestBuilder = new SpreedlyRequestBuilder(_parameters.ApiVersion, _parameters.SiteName);
            _statusResolver = new StatusResolver();
        }

        public SpreedlyResponse<SubscriberList> GetSubscribers()
        {
            var client = GetClient();
            return client.Get<SubscriberList>("subscribers.xml");
        }

        public SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId)
        {
            var client = GetClient();
            return client.Get<Subscriber>(string.Format("subscribers/{0}.xml", customerId));
        }

        public SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber)
        {
            var client = GetClient();
            return client.Post("subscribers.xml", newSubscriber);
        }

        public SpreedlyStatus CancelSubscriptionByCustomerId(string customerId)
        {
            var client = GetClient();
            var response = client.Post<Subscriber>(string.Format("subscribers/{0}/stop_auto_renew.xml", customerId), null);
            return response.Status;
        }

        public SpreedlyStatus UpdateSubscriber(Subscriber subscriber)
        {
            var client = GetClient();
            var response = client.Put<Subscriber>(string.Format("subscribers/{0}.xml", subscriber.CustomerId), subscriber);
            return response.Status;
        }

        private SpreedlyClient GetClient()
        {
            return new SpreedlyClient(_parameters.ApiKey, "X", _requestBuilder, _statusResolver);
        }
    }
}