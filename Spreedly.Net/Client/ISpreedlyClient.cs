namespace Spreedly.Net.Client
{
    public interface ISpreedlyClient
    {
        SpreedlyResponse<TGet> Get<TGet>(string urlActionSegment) where TGet : new();
        SpreedlyResponse Post(string urlActionSegment, object postObject);
        SpreedlyResponse Post(string urlActionSegment);
        SpreedlyResponse<TResponse> Post<TResponse>(string urlActionSegment, object postObject) where TResponse : new();
        SpreedlyResponse<TResponse> Post<TResponse>(string urlActionSegment) where TResponse : new();
        SpreedlyResponse<TResponse> Put<TResponse>(string urlActionSegment, object putObject) where TResponse : new();
        SpreedlyResponse Delete(string urlActionSegment);
    }
}