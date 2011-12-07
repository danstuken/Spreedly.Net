namespace Spreedly.Client
{
    using RestSharp;

    public interface IRequestBuilder
    {
        RestRequest BuildGetRequest(string urlActionSegment);
        RestRequest BuildPutRequest(string urlActionSegment, object putData);
        RestRequest BuildPostRequest(string urlActionSegment, object postData);
        RestRequest BuildDeleteRequest(string urlActionSegment);
    }
}