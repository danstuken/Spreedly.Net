namespace Spreedly.Client
{
    using System.Text;

    using RestSharp;
    using RestSharp.Serializers;

    using Spreedly.Xml;

    internal class SpreedlyRequestBuilder : IRequestBuilder
    {
        private string _apiVersion;
        private string _siteName;

        private const string DefaultAPIVersion = "v4";

        internal SpreedlyRequestBuilder(string siteName)
            : this(DefaultAPIVersion, siteName)
        {
        }

        internal SpreedlyRequestBuilder(string apiVersion, string siteName)
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
            if (postData != null)
                request.AddBody(postData);
            return request;
        }

        public RestRequest BuildDeleteRequest(string urlActionSegment)
        {
            return BuildRequest(Method.DELETE, urlActionSegment);
        }

        public RestRequest BuildPutRequest(string actionUrlSegment, object putData)
        {
            var request = BuildRequest(Method.PUT, actionUrlSegment);
            if (putData != null)
                request.AddBody(putData);
            return request;
        }

        private RestRequest BuildRequest(Method requestMethod, string urlSegment)
        {
            var request = new RestRequest("api/{version}/{site}/{action}", requestMethod);
            request.AddUrlSegment("version", _apiVersion);
            request.AddUrlSegment("site", _siteName);
            request.AddUrlSegment("action", urlSegment);
            request.XmlSerializer = GetFrameworkSerializer();
            return request;
        }

        private ISerializer GetFrameworkSerializer()
        {
            var fwSerializer = new FrameworkSerializer();
            fwSerializer.Encoding = Encoding.UTF8;
            fwSerializer.ContentType = "text/xml";
            fwSerializer.Namespace = string.Empty;
            fwSerializer.RootElement = string.Empty;
            return fwSerializer;
        }
    }
}