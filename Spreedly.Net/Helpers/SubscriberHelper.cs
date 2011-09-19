namespace Spreedly.Net.Helpers
{
    using System.Linq;
    using Api;
    using Client;
    using Entities;

    public class SubscriberHelper
    {
        private ISpreedlySubscribers _subscribersClient;
        private ISpreedlyInvoices _invoicesClient;
        private ISpreedlySubscriptionPlans _subscriptionPlansClient;

        private static SpreedlyClientFactory _cachedClientFactory;
        private static T  GetBadClientCaller<T>(string siteName, string apiKey)
        {
            if(_cachedClientFactory == null)
                _cachedClientFactory = new SpreedlyClientFactory(apiKey, siteName);
            return _cachedClientFactory.GetClient<T>();
        }

        public SubscriberHelper(string siteName, string apiKey): this(
            GetBadClientCaller<ISpreedlySubscribers>(siteName, apiKey), 
            GetBadClientCaller<ISpreedlyInvoices>(siteName, apiKey), 
            GetBadClientCaller<ISpreedlySubscriptionPlans>(siteName, apiKey))
        {
        }

        public SubscriberHelper(ISpreedlySubscribers subscribersClient, ISpreedlyInvoices invoicesClient, ISpreedlySubscriptionPlans subscriptionPlansClient)
        {
            _subscribersClient = subscribersClient;
            _invoicesClient = invoicesClient;
            _subscriptionPlansClient = subscriptionPlansClient;
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

        public Invoice SubscribeToSubscriptionPlanWithCreditCard(Subscriber subscriber, string featureLevel, CreditCard creditCard)
        {
            var plans = _subscriptionPlansClient.GetSubscriptionPlans();
            var featureLevelPlan = plans.Entity.SubscriptionPlans.FirstOrDefault(p => p.FeatureLevel == featureLevel);
            
            if(featureLevelPlan == null)
                throw new SubscriberHelperException(string.Format("Subscription Plan with Feature Level {0} not found", featureLevel), null);

            var invoiceResponse = _invoicesClient.CreateInvoice(new Invoice
                                                                    {
                                                                        SubscriptionPlanId = featureLevelPlan.Id,
                                                                        Subscriber = subscriber
                                                                    });

            if(invoiceResponse.Status != SpreedlyStatus.Created)
                throw new SubscriberHelperException("Failed to create invoice.", invoiceResponse.Error);

            var paidInvoiceResponse = _invoicesClient.PayInvoice(invoiceResponse.Entity, new Payment
                                                                                             {
                                                                                                 AccountType = "credit-card",
                                                                                                 CreditCard = creditCard
                                                                                             });
            if(paidInvoiceResponse.Status != SpreedlyStatus.Ok)
                throw new SubscriberHelperException("Error closing subscription invoice", invoiceResponse.Error);

            return paidInvoiceResponse.Entity;
        }

    }
}