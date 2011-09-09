namespace Spreedly.Net.Client
{
    using System;
    using System.Net;
    using RestSharp;
    using System.Linq;
    using Xml;

    public class SpreedlyClient: ISpreedlyClient
    {
        private string _userName;
        private string _password;
        private IRequestBuilder _requestBuilder;
        private IStatusResolver _statusResolver;

        private IRestClient _restClient;

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
            _restClient = GetClient();
        }

        public SpreedlyResponse<TEntity> Get<TEntity>(string urlActionSegment) where TEntity: new()
        {
            return TryRequest<TEntity>(_requestBuilder.BuildGetRequest(urlActionSegment));
        }

        public SpreedlyResponse<TEntity> Post<TEntity>(string urlActionSegment, TEntity postObject) where TEntity : new()
        {
            return Post<TEntity,TEntity>(urlActionSegment, postObject);
        }

        public SpreedlyResponse<TResponse> Post<TPost, TResponse>(string urlActionSegment, TPost postObject) where TResponse : new()
        {
            return TryRequest<TResponse>(_requestBuilder.BuildPostRequest(urlActionSegment, postObject));
        }

        public SpreedlyResponse<TEntity> Put<TEntity>(string urlActionSegment, TEntity putObject) where TEntity : new()
        {
            return Put<TEntity, TEntity>(urlActionSegment, putObject);
        }

        public SpreedlyResponse<TResponse> Put<TPut, TResponse>(string urlActionSegment, TPut putObject) where TResponse : new()
        {
            return TryRequest<TResponse>(_requestBuilder.BuildPutRequest(urlActionSegment, putObject));
        }

        public SpreedlyResponse Delete(string urlActionSegment)
        {
            return TryRequest(_requestBuilder.BuildDeleteRequest(urlActionSegment));
        }

        private SpreedlyResponse TryRequest(RestRequest request)
        {
            try
            {
                var clientResponse = _restClient.Execute(request);
                var response = new SpreedlyResponse();
                PopulateResponse(clientResponse, response);
                return response;
            }
            catch (WebException ex)
            {
                return new SpreedlyResponse
                {
                    Status = SpreedlyStatus.UnspecifiedError,
                    Error = ex
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private SpreedlyResponse<TEntity> TryRequest<TEntity>(RestRequest request) where TEntity: new()
        {
            try
            {
                var clientResponse = _restClient.Execute<TEntity>(request);
                var response = new SpreedlyResponse<TEntity>();
                response.Entity = clientResponse.Data;
                PopulateResponse(clientResponse, response);
                return response;
            }
            catch (WebException ex)
            {
                return new SpreedlyResponse<TEntity>
                           {
                               Status = SpreedlyStatus.UnspecifiedError,
                               Error = ex
                           };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void PopulateResponse(IRestResponse restResponse, SpreedlyResponse outResponse)
        {
            outResponse.Status = _statusResolver.Resolve(restResponse.Headers.FirstOrDefault(hdr => hdr.Type == ParameterType.HttpHeader && hdr.Name == "Status"));
            if (restResponse.ResponseStatus == ResponseStatus.Error)
                outResponse.Error = restResponse.ErrorException;
        }

        private RestClient GetClient()
        {
            var client = new RestClient(BaseSpreedlyUrl);
            client.Authenticator = new HttpBasicAuthenticator(_userName, _password);
            client.RemoveHandler("application/xml");
            client.RemoveHandler("text/xml");
            client.AddHandler("application/xml", new FrameworkDeserializer());
            client.AddHandler("text/xml", new FrameworkDeserializer());
            return client;
        }

    }
}