namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public interface ISpreedlySubscriptionPlans
    {
        SpreedlyResponse<SubscriptionPlanList> GetSubscriptionPlans();
    }
}