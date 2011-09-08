namespace Spreedly.Net.Client
{
    using System;
    using System.Net;
    using RestSharp;
    using System.Linq;

    public class SpreedlyClient: ISpreedlyClient
    {
        private string _userName;
        private string _password;
        private IRequestBuilder _requestBuilder;
        private IStatusResolver _statusResolver;

        private const string BaseSpreedlyUrl = "https://spreedly.com";

        public SpreedlyClient(string username, string password, string apiVersion)
            :this(username, password, new SpreedlyRequestBuilder(apiVersion), new StatusResolver())
        {
        }

        public SpreedlyClient(string username, string password, IRequestBuilder requestBuilder, IStatusResolver statusResolver)
        {
            _userName = username;
            _password = password;
            _requestBuilder = requestBuilder;
            _statusResolver = statusResolver;
        }

        public SpreedlyResponse<TEntity> Get<TEntity>(string urlActionSegment) where TEntity: new()
        {
            return TryRequest<TEntity>(_requestBuilder.BuildGetRequest(urlActionSegment));
        }

        public SpreedlyResponse<TEntity> Post<TEntity>(string urlActionSegment, TEntity postObject) where TEntity : new()
        {
            return TryRequest<TEntity>(_requestBuilder.BuildPostRequest(urlActionSegment, postObject));
        }

        public SpreedlyResponse<TEntity> Put<TEntity>(string urlActionSegment, TEntity putObject) where TEntity : new()
        {
            return TryRequest<TEntity>(_requestBuilder.BuildPutRequest(urlActionSegment, putObject));
        }

        private SpreedlyResponse<TEntity> TryRequest<TEntity>(RestRequest request) where TEntity: new()
        {
            try
            {
                return GetResponse(DoRequest<TEntity>(request));
            }
            catch (WebException ex)
            {
                return new SpreedlyResponse<TEntity>
                           {
                               Status = SpreedlyStatus.UnspecifiedError,
                               Error = ex,
                               Entity = default(TEntity)
                           };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private RestResponse<TEntity> DoRequest<TEntity>(RestRequest request) where TEntity: new()
        {
            var client = GetClient();
            return client.Execute<TEntity>(request);
        }

        private SpreedlyResponse<TEntity> GetResponse<TEntity>(RestResponse<TEntity> restResponse)
        {
            var response = new SpreedlyResponse<TEntity>();
            response.Entity = restResponse.Data;
            response.Status = _statusResolver.Resolve(restResponse.Headers.FirstOrDefault(hdr => hdr.Type == ParameterType.HttpHeader && hdr.Name == "Status"));
            if (restResponse.ResponseStatus == ResponseStatus.Error)
                response.Error = restResponse.ErrorException;
            return response;
        }

        private RestClient GetClient()
        {
            var client = new RestClient(BaseSpreedlyUrl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _password);
            return client;
        }
    }
}