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

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateSubscription_WithNotFoundForCreate_ThrowsNotFoundException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetForExistingPlanAndStatusReturnInCreateInvoice(existingFeatureLevel, SpreedlyStatus.NotFound);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(UnprocessableEntityException))]
        public void CreateSubscription_WithUnprocesseableEntityForCreate_ThrowsUnprocessableEntityException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetForExistingPlanAndStatusReturnInCreateInvoice(existingFeatureLevel, SpreedlyStatus.UnprocessableEntity);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithForbiddenStatusForCreate_ThrowsForbiddenActionException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetForExistingPlanAndStatusReturnInCreateInvoice(existingFeatureLevel, SpreedlyStatus.Forbidden);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithForbiddenStatusForPay_ThrowsForbiddenActionException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetForExistingPlanAndStatusReturnInPayInvoice(existingFeatureLevel, SpreedlyStatus.Forbidden);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(UnprocessableEntityException))]
        public void CreateSubscription_WithUnprocessableEntityStatusForPay_ThrowsUnprocessableEntityException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetForExistingPlanAndStatusReturnInPayInvoice(existingFeatureLevel, SpreedlyStatus.UnprocessableEntity);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(PaymentGatewayTimeoutException))]
        public void CreateSubscription_WithGatewayTimeoutStatusForPay_ThrowsPaymentGatewayException()
        {
            var existingFeatureLevel = "FeatureLevelExists";
            var subscriber = new Subscriber
                                 {
                                     CustomerId = "Id"
                                 };
            SetForExistingPlanAndStatusReturnInPayInvoice(existingFeatureLevel, SpreedlyStatus.GatewayTimeout);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        existingFeatureLevel,
                                                                        new CreditCard());
        }

        [Test]
        public void CreateFreeTrialSubscription_ForExistingPlan_ReturnsSubscriber()
        {
            var freeTrialFeatureLevel = "FreeLevel";
            var customerId = "111";
            var subscriber = new Subscriber
                                 {
                                     CustomerId = customerId
                                 };
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                {
                                                                                    Status = SpreedlyStatus.Ok,
                                                                                    Entity = new SubscriptionPlanList
                                                                                    {
                                                                                        SubscriptionPlans = new List<SubscriptionPlan>
                                                                                                                {
                                                                                                                    new SubscriptionPlan{ FeatureLevel = freeTrialFeatureLevel }
                                                                                                                }
                                                                                    }

                                                                                });
            _subscriberClient.SubscribeSubscriberToFreeTrial(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.Ok,
                                                                                                   Entity = new Subscriber()
                                                                                               });

            subscriber = _subscriberHelper.SubscribeToFreeTrialPlan(subscriber, freeTrialFeatureLevel);

            Assert.NotNull(subscriber);
        }

        [Test]
        public void CreateFreeTrialSubscription_ForExistingPlan_PassesCorrectSubscriber()
        {
            var freeTrialFeatureLevel = "FreeLevel";
            var customerId = "111";
            var subscriber = new Subscriber
                                 {
                                     CustomerId = customerId
                                 };
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                {
                                                                                    Status = SpreedlyStatus.Ok,
                                                                                    Entity = new SubscriptionPlanList
                                                                                    {
                                                                                        SubscriptionPlans = new List<SubscriptionPlan>
                                                                                                                {
                                                                                                                    new SubscriptionPlan{ FeatureLevel = freeTrialFeatureLevel }
                                                                                                                }
                                                                                    }

                                                                                });
            _subscriberClient.SubscribeSubscriberToFreeTrial(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.Ok,
                                                                                                   Entity = new Subscriber()
                                                                                               });

            _subscriberHelper.SubscribeToFreeTrialPlan(subscriber, freeTrialFeatureLevel);

            _subscriberClient.Received().SubscribeSubscriberToFreeTrial(
                Arg.Is<string>(s => s == customerId),
                Arg.Is<SubscriptionPlan>(sp => sp.FeatureLevel == freeTrialFeatureLevel));
        }

        private void SetForExistingPlanAndStatusReturnInPayInvoice(string featureLevel, SpreedlyStatus status)
        {
            
            SetForExistingPlanAndStatusReturnInCreateInvoice(featureLevel, SpreedlyStatus.Created);
            _paymentsClient.PayInvoice(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                         {
                                                                             Status = status,
                                                                             Entity = null
                                                                         });
        }

        private void SetForExistingPlanAndStatusReturnInCreateInvoice(string featureLevel, SpreedlyStatus status)
        {
            _subscriptionPlansClient.GetSubscriptionPlans().ReturnsForAnyArgs(new SpreedlyResponse<SubscriptionPlanList>
                                                                                {
                                                                                    Status = SpreedlyStatus.Ok,
                                                                                    Entity = new SubscriptionPlanList
                                                                                    {
                                                                                        SubscriptionPlans = new List<SubscriptionPlan>()
                                                                                                                {
                                                                                                                    new SubscriptionPlan
                                                                                                                        {
                                                                                                                            FeatureLevel = featureLevel
                                                                                                                        }
                                                                                                                }
                                                                                    }
                                                                                });
            _paymentsClient.CreateInvoice(null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                      {
                                                                          Status = status,
                                                                          Entity = new Invoice()
                                                                      });
        }
    }

}