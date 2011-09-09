namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public interface ISpreedlySubscribers
    {
        SpreedlyResponse<SubscriberList> GetSubscribers();
        SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId);
        SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber);
        SpreedlyResponse<Subscriber> CancelSubscriptionByCustomerId(string customerId);
        SpreedlyResponse<Subscriber> UpdateSubscriber(Subscriber subscriber);
        SpreedlyResponse DeleteSubscriber(string customerId);
        SpreedlyResponse DeleteAllSubscribers();
    }
}