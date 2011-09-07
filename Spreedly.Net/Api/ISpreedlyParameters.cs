namespace Spreedly.Net.Api
{
    public interface ISpreedlyParameters
    {
        string ApiVersion { get; }
        string ApiKey { get; }
        string SiteName { get; }
    }
}