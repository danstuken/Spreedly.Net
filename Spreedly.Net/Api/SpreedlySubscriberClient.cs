namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public class SpreedlySubscriberClient: ISpreedlySubscribers
    {
        private ISpreedlyClient _client;

        public SpreedlySubscriberClient(ISpreedlyParameters parameters): this(GetSpreedlyClient(parameters))
        {
        }

        public SpreedlySubscriberClient(ISpreedlyClient client)
        {
            _client = client;
        }

        public SpreedlyResponse<SubscriberList> GetSubscribers()
        {
            return _client.Get<SubscriberList>("subscribers.xml");
        }

        public SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId)
        {
            return _client.Get<Subscriber>(string.Format("subscribers/{0}.xml", customerId));
        }

        public SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber)
        {
            return _client.Post("subscribers.xml", newSubscriber);
        }

        public SpreedlyStatus CancelSubscriptionByCustomerId(string customerId)
        {
            var response = _client.Post<Subscriber>(string.Format("subscribers/{0}/stop_auto_renew.xml", customerId), null);
            return response.Status;
        }

        public SpreedlyStatus UpdateSubscriber(Subscriber subscriber)
        {
            var response = _client.Put<Subscriber>(string.Format("subscribers/{0}.xml", subscriber.CustomerId), subscriber);
            return response.Status;
        }

        private static SpreedlyClient GetSpreedlyClient(ISpreedlyParameters parameters)
        {
            return new SpreedlyClient(parameters.ApiKey, "X",
                                      new SpreedlyRequestBuilder(parameters.ApiVersion, parameters.SiteName),
                                      new StatusResolver());
        }
    }
}