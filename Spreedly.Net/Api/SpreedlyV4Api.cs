namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public class SpreedlyV4Api: ISpreedlyInvoices, ISpreedlySubscribers, ISpreedlySubscriptionPlans
    {
        private ISpreedlyClient _client;

        internal SpreedlyV4Api(ISpreedlyClient client)
        {
            _client = client;
        }

        public SpreedlyResponse<Invoice> CreateInvoice(Invoice invoice)
        {
            return _client.Post("invoices.xml", invoice);
        }

        public SpreedlyResponse<Invoice> PayInvoice(Invoice invoice, Payment payment)
        {
            var urlSegment = string.Format("invoices/{0}/pay.xml", invoice.SubscriptionPlanId);
            return _client.Put<Payment, Invoice>(urlSegment, payment);
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
            return _client.Post("subscribers.xml", newSubscriber);
        }

        public SpreedlyResponse<Subscriber> CancelSubscriptionByCustomerId(string customerId)
        {
            return _client.Post<Subscriber>(string.Format("subscribers/{0}/stop_auto_renew.xml", customerId), null);
        }

        public SpreedlyResponse<Subscriber> UpdateSubscriber(Subscriber subscriber)
        {
            return _client.Put(string.Format("subscribers/{0}.xml", subscriber.CustomerId), subscriber);
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