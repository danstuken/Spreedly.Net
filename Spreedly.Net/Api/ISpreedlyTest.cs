namespace Spreedly.Net.Api
{
    using Client;

    public interface ISpreedlyTest
    {
        SpreedlyResponse DeleteSubscriber(string customerId);
        SpreedlyResponse DeleteAllSubscribers(); 
    }
}