namespace Spreedly.Net.Entities
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "subscription-plans")]
    public class SubscriptionPlanList
    {
        [XmlElement(ElementName = "subscription-plan")]
        public List<SubscriptionPlan> SubscriptionPlans { get; set; }

        public SubscriptionPlanList()
        {
            SubscriptionPlans = new List<SubscriptionPlan>();
        } 
    }
}