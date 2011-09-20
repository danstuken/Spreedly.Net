namespace Spreedly.Net.Helpers.Exceptions
{
    using System;

    public class SubscriberHelperException : Exception
    {
        public string ServerResponseText { get; set; }

        public SubscriberHelperException(string message, Exception innerException)
            : this(message, string.Empty, innerException)
        {
        }

        public SubscriberHelperException(string message, string serverResponseText, Exception innerException)
            : base(message, innerException)
        {
            ServerResponseText = serverResponseText;
        }
    }
}