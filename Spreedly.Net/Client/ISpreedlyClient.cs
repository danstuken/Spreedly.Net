namespace Spreedly.Net.Client
{
    public interface ISpreedlyClient
    {
        public SpreedlyResponse<TEntity> Get<TEntity>(string urlActionSegment) where TEntity : new();
        public SpreedlyResponse<TEntity> Post<TEntity>(string urlActionSegment, TEntity postObject) where TEntity : new();
        public SpreedlyResponse<TEntity> Put<TEntity>(string urlActionSegment, TEntity putObject) where TEntity : new();
    }
}