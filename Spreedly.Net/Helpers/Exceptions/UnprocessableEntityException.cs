namespace Spreedly.Net.Helpers.Exceptions
{
    using System;

    public class UnprocessableEntityException : Exception
    {
        public string DetailMessage { get; set; }

        public UnprocessableEntityException(string message, string detail)
            : base(message)
        {
            DetailMessage = detail;
            Data.Add("DetailMessage", detail);
        }
    }
}