namespace Spreedly.Net.Helpers
{
    using System.Linq;
    using Api;
    using Client;
    using Entities;
    using Exceptions;

    public class SubscriberHelper
    {
        private ISpreedlySubscribers _subscribersClient;
        private ISpreedlyInvoices _invoicesClient;
        private ISpreedlySubscriptionPlans _subscriptionPlansClient;

        private static SpreedlyClientFactory _cachedClientFactory;
        private static T GetBadClientCaller<T>(string siteName, string apiKey)
        {
            if (_cachedClientFactory == null)
                _cachedClientFactory = new SpreedlyClientFactory(apiKey, siteName);
            return _cachedClientFactory.GetClient<T>();
        }

        public SubscriberHelper(string siteName, string apiKey)
            : this(
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

        public Subscriber FetchSubscriber(string customerId)
        {
            var subscriber = _subscribersClient.GetSubscriberByCustomerId(customerId);
            if (subscriber.Status == SpreedlyStatus.Ok)
                return subscriber.Entity;
            if (subscriber.Status == SpreedlyStatus.NotFound)
                throw new NotFoundException(string.Format("Subscriber {0} not found", customerId));
            throw new SubscriberHelperException(string.Format("Unexpected error fetching subscriber {0}", customerId), subscriber.RawBody, subscriber.Error);
        }

        public Subscriber CreateSubscriber(string customerId, string emailAddress, string screenName)
        {
            var subscriber = _subscribersClient.CreateSubscriber(new Subscriber
            {
                CustomerId = customerId,
                Email = emailAddress,
                ScreenName = screenName
            });

            if (subscriber.Status != SpreedlyStatus.Created)
                throw new SubscriberHelperException(string.Format("Unexpected error creating subscriber {0}", customerId), subscriber.RawBody, subscriber.Error);
            return subscriber.Entity;
        }

        public Invoice SubscribeToSubscriptionPlanWithCreditCard(Subscriber subscriber, string featureLevel, CreditCard creditCard)
        {
            var featureLevelPlan = GetPlanFromFeatureLevel(featureLevel);
            if (featureLevelPlan == null)
                throw new SubscriberHelperException(string.Format("Subscription Plan with Feature Level {0} not found", featureLevel), null);

            var invoice = CreateInvoice(featureLevelPlan.Id.Value, subscriber);
            var paidInvoiceResponse = PayInvoice(invoice, new Payment
            {
                AccountType = "credit-card",
                CreditCard = creditCard
            });
            return paidInvoiceResponse;
        }

        public Invoice ChangeSubscriberFeatureLevelWithOnFilePayment(Subscriber subscriber, string newFeatureLevel)
        {
            var featureLevelPlan = GetPlanFromFeatureLevel(newFeatureLevel);
            if (featureLevelPlan == null)
                throw new SubscriberHelperException(string.Format("Subscription Plan with Feature Level {0} not found", newFeatureLevel), null);

            var invoice = CreateInvoice(featureLevelPlan.Id.Value, subscriber);
            var paidInvoiceResponse = PayInvoice(invoice, new Payment
            {
                AccountType = "on-file"
            });
            return paidInvoiceResponse;
        }

        public Subscriber SubscribeToFreeTrialPlan(Subscriber subscriber, string freeTrialFeatureLevel)
        {
            var freePlan = GetPlanFromFeatureLevel(freeTrialFeatureLevel);
            if(freePlan == null)
                throw new SubscriberHelperException(string.Format("Subscription Plan with Feature Level {0} not found", freeTrialFeatureLevel), null);

            var subscribedSubscriber = _subscribersClient.SubscribeSubscriberToFreeTrial(subscriber.CustomerId, freePlan);

            if(subscribedSubscriber.Status == SpreedlyStatus.NotFound)
                throw new NotFoundException(string.Format("Failed to subscribe subscriber with customer id {0} to free plan. Subscriber not found.", subscriber.CustomerId));

            if(subscribedSubscriber.Status == SpreedlyStatus.UnprocessableEntity)
                throw new UnprocessableEntityException(string.Format("Failed to subscribe subscriber with customer id {0} to plan. Bad entity sent", subscriber.CustomerId), 
                    subscribedSubscriber.RawBody);

            if(subscribedSubscriber.Status == SpreedlyStatus.Forbidden)
                throw new ForbiddenActionException(string.Format("Failed to subscribe subscriber with customer id {0} to plan. Forbidden for this plan or subscriber", 
                    subscriber.CustomerId), subscribedSubscriber.RawBody);

            return subscribedSubscriber.Entity;
        }

        private SubscriptionPlan GetPlanFromFeatureLevel(string featureLevel)
        {
            var plans = _subscriptionPlansClient.GetSubscriptionPlans();
            return plans.Entity.SubscriptionPlans.FirstOrDefault(p => p.FeatureLevel == featureLevel);
        }

        private Invoice CreateInvoice(int featureLevelPlanId, Subscriber subscriber)
        {
            var invoiceResponse = _invoicesClient.CreateInvoice(new Invoice
            {
                SubscriptionPlanId = featureLevelPlanId,
                Subscriber = subscriber
            });

            if(invoiceResponse.Status == SpreedlyStatus.Forbidden)
                throw new ForbiddenActionException(string.Format("Failed to create invoice for subscription plan with id, {0}", featureLevelPlanId), invoiceResponse.RawBody);
            if(invoiceResponse.Status == SpreedlyStatus.NotFound)
                throw new NotFoundException(string.Format("Failed to create invoice. No plan found for id {0}", featureLevelPlanId));
            if(invoiceResponse.Status == SpreedlyStatus.UnprocessableEntity)
                throw new UnprocessableEntityException(string.Format("Failed to create invoice for subscription plan with id {0}", featureLevelPlanId), invoiceResponse.RawBody);
            if (invoiceResponse.Status != SpreedlyStatus.Created)
                throw new SubscriberHelperException("Failed to create invoice.", invoiceResponse.RawBody, invoiceResponse.Error);

            return invoiceResponse.Entity;
        }

        private Invoice PayInvoice(Invoice invoice, Payment payment)
        {
            var paidInvoiceResponse = _invoicesClient.PayInvoice(invoice, payment);
            if (paidInvoiceResponse.Status == SpreedlyStatus.Forbidden)
                throw new ForbiddenActionException(string.Format("Error paying invoice {0}", invoice.Token),
                                                   paidInvoiceResponse.RawBody);
            if (paidInvoiceResponse.Status == SpreedlyStatus.UnprocessableEntity)
                throw new UnprocessableEntityException(string.Format("Error paying invoice {0}", invoice.Token),
                                                       paidInvoiceResponse.RawBody);
            if (paidInvoiceResponse.Status == SpreedlyStatus.GatewayTimeout)
                throw new PaymentGatewayTimeoutException(string.Format("Payment gateway timed out during payment of invoice {0}", invoice.Token));
            if (paidInvoiceResponse.Status != SpreedlyStatus.Ok)
                throw new SubscriberHelperException("Error closing subscription invoice", paidInvoiceResponse.RawBody, paidInvoiceResponse.Error);
            
            return paidInvoiceResponse.Entity;
        }
    }
}