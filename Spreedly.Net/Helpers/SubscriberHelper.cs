namespace Spreedly.Net.Helpers
{
    using Api;
    using Client;
    using Entities;

    public class SubscriberHelper
    {
        private ISpreedlySubscribers _subscribersClient;

        public SubscriberHelper(string siteName, string apiKey): this(new SpreedlyV4Api(new SpreedlyClient(apiKey, "X", new SpreedlyRequestBuilder(siteName), new StatusResolver())))
        {
        }

        public SubscriberHelper(ISpreedlySubscribers subscribersClient)
        {
            _subscribersClient = subscribersClient;
        }

        public bool Exists(string customerId)
        {
            var subscriber = _subscribersClient.GetSubscriberByCustomerId(customerId);
            return subscriber.Status == SpreedlyStatus.Ok;
        }

        public Subscriber FetchOrCreate(string customerId, string emailAddress, string screenName)
        {
            var subscriber = _subscribersClient.GetSubscriberByCustomerId(customerId);
            if (subscriber.Status == SpreedlyStatus.Ok)
                return subscriber.Entity;
            if(subscriber.Status != SpreedlyStatus.NotFound)
                throw new SubscriberHelperException(string.Format("Unexpected error fetching subscriber {0}", customerId), subscriber.Error);

            subscriber = _subscribersClient.CreateSubscriber(new Subscriber
                                                                 {
                                                                     CustomerId = customerId,
                                                                     Email = emailAddress,
                                                                     ScreenName = screenName
                                                                 });

            if(subscriber.Status != SpreedlyStatus.Created)
                throw new SubscriberHelperException(string.Format("Unexpected error creating subscriber {0}", customerId), subscriber.Error);
            return subscriber.Entity;
        }
    }
}