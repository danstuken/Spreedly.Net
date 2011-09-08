namespace Spreedly.Net.Client
{
    public interface ISpreedlyClient
    {
        SpreedlyResponse<TEntity> Get<TEntity>(string urlActionSegment) where TEntity : new();
        SpreedlyResponse<TEntity> Post<TEntity>(string urlActionSegment, TEntity postObject) where TEntity : new();
        SpreedlyResponse<TResponse> Post<TPost,TResponse>(string urlActionSegment, TPost postObject) where TResponse : new();
        SpreedlyResponse<TEntity> Put<TEntity>(string urlActionSegment, TEntity putObject) where TEntity : new();
        SpreedlyResponse<TResponse> Put<TPut, TResponse>(string urlActionSegment, TPut putObject) where TResponse : new();
    }
}