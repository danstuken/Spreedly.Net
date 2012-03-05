namespace Spreedly.Api
{
    using Spreedly.Client;
    using Spreedly.Entities;

    public class SpreedlyV4Api : ISpreedlyInvoices, ISpreedlySubscribers, ISpreedlySubscriptionPlans, ISpreedlyTest
    {
        private ISpreedlyClient _client;

        internal SpreedlyV4Api(ISpreedlyClient client)
        {
            _client = client;
        }

        public SpreedlyResponse<Invoice> CreateInvoice(Invoice invoice)
        {
            return _client.Post<Invoice>("invoices.xml", invoice);
        }

        public SpreedlyResponse<Invoice> PayInvoice(Invoice invoice, Payment payment)
        {
            var urlSegment = string.Format("invoices/{0}/pay.xml", invoice.Token);
            return _client.Put<Invoice>(urlSegment, payment);
        }

        public SpreedlyResponse<SubscriberList> GetSubscribers()
        {
            return _client.Get<SubscriberList>("subscribers.xml");
        }

        public SpreedlyResponse<Subscriber> GetSubscriberByCustomerId(string customerId)
        {
            return _client.Get<Subscriber>(string.Format("subscribers/{0}.xml", customerId));
        }

        public SpreedlyResponse<Subscriber> CreateSubscriber(Subscriber newSubscriber)
        {
            return _client.Post<Subscriber>("subscribers.xml", newSubscriber);
        }

        public SpreedlyResponse<Subscriber> StopAutoRenew(string customerId)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/stop_auto_renew.xml", customerId));
        }

        public SpreedlyResponse<Subscriber> GiveSubscriberComplimentarySubscription(string customerId, ComplimentarySubscription complimentarySubscription)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/complimentary_subscriptions.xml", customerId), complimentarySubscription);
        }

        public SpreedlyResponse<Subscriber> GiveSubscriberComplimentaryTimeExtensions(string customerId, ComplimentaryTimeExtension complimentaryTimeExtension)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/complimentary_time_extension.xml", customerId), complimentaryTimeExtension);
        }

        public SpreedlyResponse<Subscriber> GiveSubscriberLifetimeComplimentarySubscription(string customerId, LifetimeComplimentarySubscription lifetimeComplimentarySubscription)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/lifetime_complimentary_subscriptions.xml", customerId), lifetimeComplimentarySubscription);
        }

        public SpreedlyResponse GiveSubscriberStoreCredit(string customerId, StoreCredit credit)
        {
            return _client.Post(string.Format("subscribers/{0}/credits.xml", customerId), credit);
        }

        public SpreedlyResponse AddFeeToSubscriber(string customerId, Fee fee)
        {
            return _client.Post(string.Format("subscribers/{0}/fees.xml", customerId), fee);
        }

        public SpreedlyResponse<Subscriber> SubscribeSubscriberToFreeTrial(string customerId, SubscriptionPlan freePlan)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/subscribe_to_free_trial.xml", customerId), freePlan);
        }

        public SpreedlyResponse AllowSubscriberAnotherFreeTrial(string customerId)
        {
            return _client.Post(string.Format("subscribers/{0}/allow_free_trial.xml", customerId));
        }

        public SpreedlyResponse<Subscriber> UpdateSubscriber(Subscriber subscriber)
        {
            var customerId = subscriber.CustomerId;
            subscriber.CustomerId = null;
            return _client.Put<Subscriber>(string.Format("subscribers/{0}.xml", customerId), subscriber);
        }

        public SpreedlyResponse DeleteSubscriber(string customerId)
        {
            return _client.Delete(string.Format("subscribers/{0}.xml", customerId));
        }

        public SpreedlyResponse DeleteAllSubscribers()
        {
            return _client.Delete("subscribers.xml");
        }

        public SpreedlyResponse<SubscriptionPlanList> GetSubscriptionPlans()
        {
            return _client.Get<SubscriptionPlanList>("subscription_plans.xml");
        }
    }
}