namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public interface ISpreedlySubscribers
    {
        SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId);
        SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber);
        SpreedlyResponse<Subscriber> UpdateSubscriber(Subscriber subscriber);
        SpreedlyResponse<SubscriberList> GetSubscribers();
        SpreedlyResponse<Subscriber> StopAutoRenew(string customerId);
        SpreedlyResponse<Subscriber> GiveSubscriberComplimentarySubscription(string customerId, ComplimentarySubscription complimentarySubscription);
        SpreedlyResponse<Subscriber> GiveSubscriberComplimentaryTimeExtensions(string customerId, ComplimentaryTimeExtension complimentaryTimeExtension);
        SpreedlyResponse<Subscriber> GiveSubscriberLifetimeComplimentarySubscription(string customerId, LifetimeComplimentarySubscription lifetimeComplimentarySubscription);
        SpreedlyResponse GiveSubscriberStoreCredit(string customerId, StoreCredit credit);
        SpreedlyResponse AddFeeToSubscriber(string customerId, Fee fee);
        SpreedlyResponse<Subscriber> SubscribeSubscriberToFreeTrial(string customerId, SubscriptionPlan freePlan);
        SpreedlyResponse AllowSubscriberAnotherFreeTrial(string customerId);
    }
}