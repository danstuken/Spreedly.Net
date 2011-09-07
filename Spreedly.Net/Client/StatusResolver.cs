namespace Spreedly.Net.Client
{
    using System;
    using RestSharp;

    public class StatusResolver: IStatusResolver
    {
        public SpreedlyStatus Resolve(Parameter statusHeader)
        {
            if (statusHeader == null)
                throw new ArgumentNullException("statusHeader");

            return ResolveStatusCode(GetStatusCodeString(statusHeader.Value.ToString()));
        }

        private SpreedlyStatus ResolveStatusCode(string statusCode)
        {
            switch(statusCode)
            {
                case "200":
                    return SpreedlyStatus.Ok;
                case "201":
                    return SpreedlyStatus.Created;
                case "403":
                    return SpreedlyStatus.Forbidden;
                case "404":
                    return SpreedlyStatus.NotFound;
                case "422":
                    return SpreedlyStatus.UnprocessableEntity;
                case "500":
                    return SpreedlyStatus.ServerError;
                case "504":
                    return SpreedlyStatus.GatewayTimeout;
            }
            return SpreedlyStatus.UnspecifiedError;
        }

        private string GetStatusCodeString(string rawStatusHeader)
        {
            return rawStatusHeader.Trim().Split(new[] {' ', '\t'})[0];
        }
    }
}