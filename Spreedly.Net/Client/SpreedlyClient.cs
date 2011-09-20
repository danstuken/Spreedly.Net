namespace Spreedly.Net.Client
{
    using System;
    using System.Net;
    using RestSharp;
    using System.Linq;
    using Xml;

    internal class SpreedlyClient: ISpreedlyClient
    {
        private string _userName;
        private string _password;
        private IRequestBuilder _requestBuilder;
        private IStatusResolver _statusResolver;

        private IRestClient _restClient;

        private const string BaseSpreedlyUrl = "https://spreedly.com";

        internal SpreedlyClient(string username, string password, string siteName)
            :this(username, password, new SpreedlyRequestBuilder(siteName), new StatusResolver())
        {
        }

        internal SpreedlyClient(string username, string password, IRequestBuilder requestBuilder, IStatusResolver statusResolver)
        {
            _userName = username;
            _password = password;
            _requestBuilder = requestBuilder;
            _statusResolver = statusResolver;
            _restClient = GetClient();
        }

        public SpreedlyResponse<TGet> Get<TGet>(string urlActionSegment) where TGet: new()
        {
            return TryRequest<TGet>(_requestBuilder.BuildGetRequest(urlActionSegment));
        }

        public SpreedlyResponse Post(string urlActionSegment)
        {
            return TryRequest(_requestBuilder.BuildPostRequest(urlActionSegment, null));
        }

        public SpreedlyResponse Post(string urlActionSegment, object postObject)
        {
            return TryRequest(_requestBuilder.BuildPostRequest(urlActionSegment, postObject));
        }

        public SpreedlyResponse<TResponse> Post<TResponse>(string urlActionSegment, object postObject) where TResponse : new()
        {
            return TryRequest<TResponse>(_requestBuilder.BuildPostRequest(urlActionSegment, postObject));
        }

        public SpreedlyResponse<TResponse> Post<TResponse>(string urlActionSegment) where TResponse : new()
        {
            return TryRequest<TResponse>(_requestBuilder.BuildPostRequest(urlActionSegment, null));
        }

        public SpreedlyResponse<TResponse> Put<TResponse>(string urlActionSegment, object putObject) where TResponse : new()
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
            catch (Exception)
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
            catch (Exception)
            {
                throw;
            }
        }

        private void PopulateResponse(IRestResponse restResponse, SpreedlyResponse outResponse)
        {
            outResponse.Status = _statusResolver.Resolve(restResponse.Headers.FirstOrDefault(hdr => hdr.Type == ParameterType.HttpHeader && hdr.Name == "Status"));
            if (restResponse.ResponseStatus == ResponseStatus.Error)
                outResponse.Error = restResponse.ErrorException;
            outResponse.RawBody = restResponse.Content;
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