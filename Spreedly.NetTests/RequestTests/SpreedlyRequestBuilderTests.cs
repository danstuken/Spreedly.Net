namespace Spreedly.NetTests.RequestTests
{
    using System.Linq;
    using NUnit.Framework;

    using RestSharp;
    using Shouldly;

    [TestFixture]
    public class SpreedlyRequestBuilderTests
    {
        [Test]
        public void CreatingPostRequest_WithNullObject_DoesNotSetBody()
        {
            var requestBuilder = new SpreedlyRequestBuilder("blah");

            var request = requestBuilder.BuildPostRequest("blah", null);

            request.Parameters.Where(p => p.Type == ParameterType.RequestBody).ShouldBeEmpty();
        }
         
    }
}