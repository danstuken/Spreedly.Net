namespace Spreedly.Net.Helpers
{
    using System;

    public class HelperException: Exception
    {
         public HelperException(string message, Exception innerException): base(message, innerException)
         {
         }
    }
}