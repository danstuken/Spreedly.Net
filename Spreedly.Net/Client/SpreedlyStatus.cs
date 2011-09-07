namespace Spreedly.Net.Client
{
    public enum SpreedlyStatus
    {
        Ok = 200,
        Created = 201,
        Forbidden = 403,
        NotFound = 404,
        UnprocessableEntity = 422,
        ServerError = 500,
        GatewayTimeout = 504,
        UnspecifiedError = -1
    }
}