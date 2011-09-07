namespace Spreedly.Net.Client
{
    using RestSharp;

    public class SpreedlyRequestBuilder: IRequestBuilder
    {
        private string _apiVersion;
        private string _siteName;

        private const string DefaultAPIVersion = "v4";

        public SpreedlyRequestBuilder(string siteName)
            :this(DefaultAPIVersion, siteName)
        {
        }

        public SpreedlyRequestBuilder(string apiVersion, string siteName)
        {
            _apiVersion = apiVersion;
            _siteName = siteName;
        }

        public RestRequest BuildGetRequest(string actionUrlSegment)
        {
            return BuildRequest(Method.GET, actionUrlSegment);
        }

        public RestRequest BuildPostRequest(string actionUrlSegment, object postData)
        {
            var request = BuildRequest(Method.POST, actionUrlSegment);
            request.AddBody(postData);
            return request;
        }

        public RestRequest BuildPutRequest(string actionUrlSegment, object putData)
        {
            var request = BuildRequest(Method.PUT, actionUrlSegment);
            request.AddBody(putData);
            return request;
        }

        private RestRequest BuildRequest(Method requestMethod, string urlSegment)
        {
            var request = new RestRequest("api/{version}/{site}/{action}", requestMethod);
            request.AddUrlSegment("version", _apiVersion);
            request.AddUrlSegment("site", _siteName);
            request.AddUrlSegment("action", urlSegment);
            return request;
        }
    }
}