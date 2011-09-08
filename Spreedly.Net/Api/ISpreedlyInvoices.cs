namespace Spreedly.Net.Api
{
    using Client;
    using Entities;

    public interface ISpreedlyInvoices
    {
        SpreedlyResponse<Invoice> CreateInvoice(Invoice invoice);
        SpreedlyResponse<Invoice> PayInvoice(Invoice invoice, Payment payment);
    }
}