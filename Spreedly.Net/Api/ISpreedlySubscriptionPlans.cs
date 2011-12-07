namespace Spreedly.Api
{
    using Spreedly.Client;
    using Spreedly.Entities;

    public interface ISpreedlySubscriptionPlans
    {
        SpreedlyResponse<SubscriptionPlanList> GetSubscriptionPlans();
    }
}