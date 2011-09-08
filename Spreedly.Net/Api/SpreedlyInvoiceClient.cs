namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public class SpreedlyInvoiceClient : ISpreedlyInvoices
    {
        private ISpreedlyClient _client;

        public SpreedlyInvoiceClient(ISpreedlyParameters parameters) : this(GetSpreedlyClient(parameters))
        {
        }

        public SpreedlyInvoiceClient(ISpreedlyClient client)
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

        private static SpreedlyClient GetSpreedlyClient(ISpreedlyParameters parameters)
        {
            return new SpreedlyClient(parameters.ApiKey, "X",
                                      new SpreedlyRequestBuilder(parameters.ApiVersion, parameters.SiteName),
                                      new StatusResolver());
        }
    }
}