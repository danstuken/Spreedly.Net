namespace Spreedly.Net.Helpers.Exceptions
{
    using System;

    public class PaymentGatewayTimeoutException: Exception
    {
         public PaymentGatewayTimeoutException(string message): base(message)
         {
         }
    }
}