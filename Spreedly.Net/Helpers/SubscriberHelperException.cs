namespace Spreedly.Net.Helpers
{
    using System;

    public class SubscriberHelperException: HelperException
    {
         public SubscriberHelperException(string message, Exception innerException): base(message, innerException)
         {
         }
    }
}