namespace Spreedly.Net.Helpers.Exceptions
{
    using System;

    public class NotFoundException: Exception
    {
         public NotFoundException(string message): base(message)
         {
         }
    }
}