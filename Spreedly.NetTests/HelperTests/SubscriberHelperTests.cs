namespace Spreedly.NetTests.HelperTests
{
    using System;
    using NSubstitute;
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;
    using Net.Helpers;

    [TestFixture]
    public class SubscriberHelperTests
    {
        
        [Test]
        public void ExistsReturnsTrue_ForExistingSubscriber()
        {
            var existingCustomerId = "IExist";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                             {
                                                                                                 Status = SpreedlyStatus.Ok,
                                                                                                 Entity = new Subscriber { CustomerId = existingCustomerId }
                                                                                             });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var exists = subscriberHelper.Exists(existingCustomerId);

            Assert.True(exists);
        }

        [Test]
        public void ExistsReturnsFalse_ForNonExistingSubscriber()
        {
            var nonExistingCustomerId = "IDontExist";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                             {
                                                                                                 Status = SpreedlyStatus.NotFound,
                                                                                                 Entity = null
                                                                                             });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var exists = subscriberHelper.Exists(nonExistingCustomerId);

            Assert.False(exists);
        }

        [Test]
        public void FetchOrCreateForNonExistingCustomer_CallsCreateSubscriber()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                                  {
                                                                                                      Status = SpreedlyStatus.NotFound,
                                                                                                      Entity = null
                                                                                                  });
            spreedlyClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                        {
                                                                            Status = SpreedlyStatus.Created,
                                                                            Entity = new Subscriber
                                                                                         {
                                                                                             CustomerId = nonExistingCustomerId,
                                                                                             Email = anEmailAddress,
                                                                                             ScreenName = aScreenName
                                                                                         }
                                                                        });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var subscriber = subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);

            spreedlyClient.ReceivedWithAnyArgs().CreateSubscriber(null);
        }

        [Test]
        public void FetchOrCreateForNonExistingCustomer_ReturnsSubscriberWithCorrectCustomerId()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.NotFound,
                                                                                                Entity = null
                                                                                            });
            spreedlyClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                    {
                                                                        Status = SpreedlyStatus.Created,
                                                                        Entity = new Subscriber
                                                                        {
                                                                            CustomerId = nonExistingCustomerId,
                                                                            Email = anEmailAddress,
                                                                            ScreenName = aScreenName
                                                                        }
                                                                    });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var subscriber = subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);

            Assert.AreEqual(nonExistingCustomerId, subscriber.CustomerId);
        }

        [Test]
        public void FetchOrCreateForExistingCustomer_ReturnsSubscriberWithoutCreating()
        {
            var existingCustomerId = "IExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.Ok,
                                                                                                Entity = new Subscriber
                                                                                                {
                                                                                                    CustomerId = existingCustomerId,
                                                                                                    Email = anEmailAddress,
                                                                                                    ScreenName = aScreenName
                                                                                                }
                                                                                            });

            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var subscriber = subscriberHelper.FetchOrCreate(existingCustomerId, anEmailAddress, aScreenName);

            spreedlyClient.DidNotReceiveWithAnyArgs().CreateSubscriber(null);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void FetchOrCreate_WhenFetchErrors_ThrowsSubscriberHelperException()
        {
            var existingCustomerId = "IExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var errorMessage = "Bad stuff occurred";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.ServerError,
                                                                                                   Entity = null,
                                                                                                   Error = new Exception(errorMessage)
                                                                                               });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var subscriber = subscriberHelper.FetchOrCreate(existingCustomerId, anEmailAddress, aScreenName);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void FetchOrCreate_WhenCreateErrors_ThrowsSubscriberHelperException()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var errorMessage = "Bad stuff occurred";
            var spreedlyClient = Substitute.For<ISpreedlySubscribers>();
            spreedlyClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.NotFound,
                                                                                                Entity = null
                                                                                            });
            spreedlyClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                    {
                                                                        Status = SpreedlyStatus.ServerError,
                                                                        Entity = null,
                                                                        Error =  new Exception(errorMessage)
                                                                    });
            var subscriberHelper = new SubscriberHelper(spreedlyClient);

            var subscriber = subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);
        }
    }
}