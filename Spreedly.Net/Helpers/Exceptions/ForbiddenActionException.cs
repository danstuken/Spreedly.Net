namespace Spreedly.Net.Helpers.Exceptions
{
    using System;

    public class ForbiddenActionException: Exception
    {
        public string Reason { get; set; }

        public ForbiddenActionException(string message, string reason): base(message)
        {
            Reason = reason;
        }
    }
}