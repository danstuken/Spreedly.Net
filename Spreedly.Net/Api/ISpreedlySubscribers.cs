namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public interface ISpreedlySubscribers
    {
        SpreedlyResponse<SubscriberList> GetSubscribers();
        SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId);
        SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber);
        SpreedlyStatus CancelSubscriptionByCustomerId(string customerId);
        SpreedlyStatus UpdateSubscriber(Subscriber subscriber);
    }
}