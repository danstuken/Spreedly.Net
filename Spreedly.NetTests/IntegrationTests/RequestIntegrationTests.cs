namespace Spreedly.NetTests.IntegrationTests
{
    using System.Linq;
    using NUnit.Framework;
    using Net.Client;
    using RestSharp;

    public class RequestIntegrationTests
    {
        [Test]
        public void AuthenticateRequestWithoutAuthHeader_AddsAuthHeader()
        {
            var client = new RestClient("www.no.where");
            var reqBuilder = new SpreedlyRequestBuilder("v1", "a-site-name");
            var request = reqBuilder.BuildPostRequest("something", "blahblah");
            client.Authenticator = new HttpBasicAuthenticator("uname", "pwd");

            client.Authenticator.Authenticate(client, request);
            Assert.True(request.Parameters.Any(p => p.Name == "Authorization"));
        } 
    }
}