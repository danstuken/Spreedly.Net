namespace Spreedly.Api
{
    using Spreedly.Client;

    public interface ISpreedlyTest
    {
        SpreedlyResponse DeleteSubscriber(string customerId);
        SpreedlyResponse DeleteAllSubscribers(); 
    }
}