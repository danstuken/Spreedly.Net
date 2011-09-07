namespace Spreedly.NetTests
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using Net.Entities;
    using RestSharp;

    [TestFixture]
    public class SimpleRequestTests
    {
        private const string ValidCardNumber = "4222222222222";
        private const string UnauthorisedCardNumber = "4012888888881881";
        private const string GatewayUnavailableCardNumber = "4111111111111111";

        [Test]
        public void RestSharpPlanFetchTest()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.GET);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "subscription_plans.xml");

            RestResponse<SubscriptionPlanList> response = client.Execute<SubscriptionPlanList>(request);
            var name = response.Data.SubscriptionPlans[0].Name;

            Assert.True(true);
        }

        [Test]
        public void RestSharpListSubscribersTest()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.GET);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "subscribers.xml");

            RestResponse<SubscriberList> response = client.Execute<SubscriberList>(request);
            
            Assert.True(true);
        }

        [Test]
        public void RestSharpCreateSubscription()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.POST);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "subscribers.xml");
            var subs = new Subscriber
                           {
                               ScreenName = "CreateTest4",
                               CustomerId = "004"
                           };
            request.AddBody(subs);

            RestResponse<Subscriber> response = client.Execute<Subscriber>(request);

            Assert.True(true);
        }

        [Test]
        public void RestSharpSetSubscriberPaymentMethod()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.PUT);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "subscribers/004.xml");

            var subs = new Subscriber
            {
                PaymentMethod = new CreditCard
                                    {
                                        Number = ValidCardNumber,
                                        CardType = "visa",
                                        ExpirationMonth = 1,
                                        ExpirationYear = 2012,
                                        FirstName = "Fred",
                                        LastName = "Jones",
                                        VerificationValue = "111"
                                    }
            };
            request.AddBody(subs);

            RestResponse<SubscriptionPlanList> response = client.Execute<SubscriptionPlanList>(request);
            var name = response.Data.SubscriptionPlans[0].Name;

            Assert.True(true);
        }

        [Test]
        public void RestSharpSetSubscriberActive()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.PUT);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "subscribers/004.xml");

            var subs = new Subscriber
            {
                FeatureLevel = "Magnum (10)"
            };
            request.AddBody(subs);

            RestResponse<SubscriptionPlanList> response = client.Execute<SubscriptionPlanList>(request);
            var name = response.Data.SubscriptionPlans[0].Name;

            Assert.True(true);
        }

        [Test]
        public void RestSharpSubscribeSubscriber()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("{site}/{action}", Method.GET);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");


            var subscriberToken = "aef789956af61024d82ec270039601b91e06262a";
            var subscriptionPlanId = "14143";
            request.AddUrlSegment("action", string.Format("subscribers/{0}/subscribe/{1}", subscriberToken, subscriptionPlanId));

            RestResponse response = client.Execute(request);

            Assert.True(true);
        }

        [Test]
        public void RestSharpCreateSubsciptionInvoice()
        {
            var client = new RestClient("https://spreedly.com");
            client.Authenticator = new HttpBasicAuthenticator("61a2304391b862d526e95f11ee7a3815f0857e3f", "X");

            var request = new RestRequest("api/{version}/{site}/{action}", Method.POST);
            request.AddUrlSegment("version", "v4");
            request.AddUrlSegment("site", "sitedocdan-test");
            request.AddUrlSegment("action", "invoices.xml");

            var invoice = new Invoice
                              {
                                  SubscriptionPlanId = 14143,
                                  Subscriber = new Subscriber
                                                   {
                                                       Email = "dan@thesitedoctor.co.uk",
                                                       ScreenName = "CreateTest4",
                                                       CustomerId = "004"
                                                   }
                              };
            request.AddBody(invoice);

            RestResponse<Invoice> response = client.Execute<Invoice>(request);

            Assert.True(true);
        }
    }
}
