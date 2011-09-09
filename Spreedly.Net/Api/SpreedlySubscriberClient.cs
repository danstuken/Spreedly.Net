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

        public SpreedlyResponse<Subscriber> CancelSubscriptionByCustomerId(string customerId)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/stop_auto_renew.xml", customerId), null);
        }

        public SpreedlyResponse<Subscriber> UpdateSubscriber(Subscriber subscriber)
        {
            return _client.Put(string.Format("subscribers/{0}.xml", subscriber.CustomerId), subscriber);
        }

        public SpreedlyResponse DeleteSubscriber(string customerId)
        {
            return _client.Delete(string.Format("subscribers/{0}.xml", customerId));
        }

        public SpreedlyResponse DeleteAllSubscribers()
        {
            return _client.Delete("subscribers.xml");
        }

        private static SpreedlyClient GetSpreedlyClient(ISpreedlyParameters parameters)
        {
            return new SpreedlyClient(parameters.ApiKey, "X",
                                      new SpreedlyRequestBuilder(parameters.ApiVersion, parameters.SiteName),
                                      new StatusResolver());
        }
    }
}