namespace Spreedly.NetTests.HelperTests
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using Net.Api;
    using Net.Client;
    using Net.Entities;
    using Net.Helpers;
    using Net.Helpers.Exceptions;

    [TestFixture]
    public class SubscriberHelperTests
    {
        private SubscriberHelper _subscriberHelper;
        private ISpreedlySubscribers _subscriberClient;
        private ISpreedlyInvoices _paymentsClient;
        private ISpreedlySubscriptionPlans _subscriptionPlansClient;

        [SetUp]
        public void Init()
        {
            _subscriberClient = Substitute.For<ISpreedlySubscribers>();
            _subscriptionPlansClient = Substitute.For<ISpreedlySubscriptionPlans>();
            _paymentsClient = Substitute.For<ISpreedlyInvoices>();
            _subscriberHelper = new SubscriberHelper(_subscriberClient, _paymentsClient, _subscriptionPlansClient);
        }
 
        [Test]
        public void ExistsReturnsTrue_ForExistingSubscriber()
        {
            var existingCustomerId = "IExist";
            _subscriberClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                             {
                                                                                                 Status = SpreedlyStatus.Ok,
                                                                                                 Entity = new Subscriber { CustomerId = existingCustomerId }
                                                                                             });
            
            var exists = _subscriberHelper.Exists(existingCustomerId);

            Assert.True(exists);
        }

        [Test]
        public void ExistsReturnsFalse_ForNonExistingSubscriber()
        {
            var nonExistingCustomerId = "IDontExist";
            _subscriberClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                             {
                                                                                                 Status = SpreedlyStatus.NotFound,
                                                                                                 Entity = null
                                                                                             });
            
            var exists = _subscriberHelper.Exists(nonExistingCustomerId);

            Assert.False(exists);
        }

        [Test]
        public void FetchOrCreateForNonExistingCustomer_CallsCreateSubscriber()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            _subscriberClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                                  {
                                                                                                      Status = SpreedlyStatus.NotFound,
                                                                                                      Entity = null
                                                                                                  });
            _subscriberClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                        {
                                                                            Status = SpreedlyStatus.Created,
                                                                            Entity = new Subscriber
                                                                                         {
                                                                                             CustomerId = nonExistingCustomerId,
                                                                                             Email = anEmailAddress,
                                                                                             ScreenName = aScreenName
                                                                                         }
                                                                        });
            
            var subscriber = _subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);

            _subscriberClient.ReceivedWithAnyArgs().CreateSubscriber(null);
        }

        [Test]
        public void FetchOrCreateForNonExistingCustomer_ReturnsSubscriberWithCorrectCustomerId()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            _subscriberClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.NotFound,
                                                                                                Entity = null
                                                                                            });
            _subscriberClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                    {
                                                                        Status = SpreedlyStatus.Created,
                                                                        Entity = new Subscriber
                                                                        {
                                                                            CustomerId = nonExistingCustomerId,
                                                                            Email = anEmailAddress,
                                                                            ScreenName = aScreenName
                                                                        }
                                                                    });
            
            var subscriber = _subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);

            Assert.AreEqual(nonExistingCustomerId, subscriber.CustomerId);
        }

        [Test]
        public void FetchOrCreateForExistingCustomer_ReturnsSubscriberWithoutCreating()
        {
            var existingCustomerId = "IExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            _subscriberClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.Ok,
                                                                                                Entity = new Subscriber
                                                                                                {
                                                                                                    CustomerId = existingCustomerId,
                                                                                                    Email = anEmailAddress,
                                                                                                    ScreenName = aScreenName
                                                                                                }
                                                                                            });
            
            var subscriber = _subscriberHelper.FetchOrCreate(existingCustomerId, anEmailAddress, aScreenName);

            _subscriberClient.DidNotReceiveWithAnyArgs().CreateSubscriber(null);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void FetchOrCreate_WhenFetchErrors_ThrowsSubscriberHelperException()
        {
            var existingCustomerId = "IExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var errorMessage = "Bad stuff occurred";
            _subscriberClient.GetSubscriberByCustomerId(existingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.ServerError,
                                                                                                   Entity = null,
                                                                                                   Error = new Exception(errorMessage)
                                                                                               });

            var subscriber = _subscriberHelper.FetchOrCreate(existingCustomerId, anEmailAddress, aScreenName);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void FetchOrCreate_WhenCreateErrors_ThrowsSubscriberHelperException()
        {
            var nonExistingCustomerId = "IDontExist";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var errorMessage = "Bad stuff occurred";
            _subscriberClient.GetSubscriberByCustomerId(nonExistingCustomerId).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                            {
                                                                                                Status = SpreedlyStatus.NotFound,
                                                                                                Entity = null
                                                                                            });
            _subscriberClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                    {
                                                                        Status = SpreedlyStatus.ServerError,
                                                                        Entity = null,
                                                                        Error =  new Exception(errorMessage)
                                                                    });

            var subscriber = _subscriberHelper.FetchOrCreate(nonExistingCustomerId, anEmailAddress, aScreenName);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void SubscribingSubscriber_ToNonExistantPlan_ThrowsSubscriberHelperException()
        {
            var nonExistingFeatureLevel = "FeatureLevelNoExist";
            var subscriber = new Subscriber
                                 {
                                     CustomerId = "Id"
                                 };
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                   {
                                                                                       Status = SpreedlyStatus.Ok,
                                                                                       Entity = new SubscriptionPlanList
                                                                                                    {
                                                                                                        SubscriptionPlans = new List<SubscriptionPlan>()
                                                                                                    }
            
                                                                                   });
            _paymentsClient.CreateInvoice(null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>());
            _paymentsClient.PayInvoice(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>());

            var paidInvoice = _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                                          nonExistingFeatureLevel,
                                                                                          new CreditCard());
        }

        [Test]
        public void SubscribingSubscriber_ToExistingPlan_ReturnsAnInvoice()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
                                 {
                                     CustomerId = "Id"
                                 };
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                {
                                                                                    Status = SpreedlyStatus.Ok,
                                                                                    Entity = new SubscriptionPlanList
                                                                                    {
                                                                                        SubscriptionPlans = new List<SubscriptionPlan>()
                                                                                                                {
                                                                                                                    new SubscriptionPlan
                                                                                                                        {
                                                                                                                            FeatureLevel = existingFeatureLevel
                                                                                                                        }
                                                                                                                }
                                                                                    }
                                                                                });
            _paymentsClient.CreateInvoice(null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                      {
                                                                          Status = SpreedlyStatus.Created,
                                                                          Entity = new Invoice()
                                                                      });
            _paymentsClient.PayInvoice(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                         {
                                                                             Status = SpreedlyStatus.Ok,
                                                                             Entity = new Invoice()
                                                                         });

            var paidInvoice = _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                                          existingFeatureLevel,
                                                                                          new CreditCard());

            Assert.NotNull(paidInvoice);
        }

        [Test]
        public void ChangingSubscriptionLevelWithOnFilePayment_PaysInvoiceUsingOnFile()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                  {
                                                                                      Status = SpreedlyStatus.Ok,
                                                                                      Entity = new SubscriptionPlanList
                                                                                                   {
                                                                                                       SubscriptionPlans = new List<SubscriptionPlan>
                                                                                                               {
                                                                                                                   new SubscriptionPlan
                                                                                                                       {
                                                                                                                           FeatureLevel = existingFeatureLevel
                                                                                                                       }
                                                                                                               }
                                                                                                   }
                                                                                  });
            _paymentsClient.CreateInvoice(null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                      {
                                                                          Status = SpreedlyStatus.Created,
                                                                          Entity = new Invoice()
                                                                      });
            _paymentsClient.PayInvoice(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                         {
                                                                             Status = SpreedlyStatus.Ok,
                                                                             Entity = new Invoice()
                                                                         });

            _subscriberHelper.ChangeSubscriberFeatureLevelWithOnFilePayment(subscriber, existingFeatureLevel);

            _paymentsClient.Received().PayInvoice(Arg.Any<Invoice>(),
                                                  Arg.Is<Payment>(p => p.AccountType == "on-file"));
        }
    }
}