namespace Spreedly.Net.Entities
{
    using System.Collections.Generic;

    public class SubscriptionPlanList
    {
        public List<SubscriptionPlan> SubscriptionPlans { get; set; }

        public SubscriptionPlanList()
        {
            SubscriptionPlans = new List<SubscriptionPlan>();
        } 
    }
}