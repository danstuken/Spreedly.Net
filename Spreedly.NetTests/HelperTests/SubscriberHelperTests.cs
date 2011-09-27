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
        public void CreateForNonExistingCustomer_CallsCreateSubscriber()
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
            
            var subscriber = _subscriberHelper.CreateSubscriber(nonExistingCustomerId, anEmailAddress, aScreenName);

            _subscriberClient.ReceivedWithAnyArgs().CreateSubscriber(null);
        }

        [Test]
        public void CreateForNonExistingCustomer_ReturnsSubscriberWithCorrectCustomerId()
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
            
            var subscriber = _subscriberHelper.CreateSubscriber(nonExistingCustomerId, anEmailAddress, aScreenName);

            Assert.AreEqual(nonExistingCustomerId, subscriber.CustomerId);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void Fetch_WhenFetchErrors_ThrowsSubscriberHelperException()
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

            var subscriber = _subscriberHelper.FetchSubscriber(existingCustomerId);
        }

        [Test]
        [ExpectedException(typeof(SubscriberHelperException))]
        public void Create_WhenFetchErrors_ThrowsSubscriberHelperException()
        {
            var somecustomerid = "SomeCustomerId";
            var anEmailAddress = "fred@fred.fred";
            var aScreenName = "FreddieFred";
            var errorMessage = "Bad stuff occurred";
            _subscriberClient.CreateSubscriber(null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
            {
                Status = SpreedlyStatus.ServerError,
                Entity = null,
                Error = new Exception(errorMessage)
            });

            var subscriber = _subscriberHelper.CreateSubscriber(somecustomerid, anEmailAddress, aScreenName);
        }

        [Test]
        public void SubscribingSubscriber_ToExistingPlan_ReturnsAnInvoice()
        {
            var subscriber = new Subscriber
                                 {
                                     CustomerId = "Id"
                                 };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Created, new Invoice());
            SetPayInvoiceStatusAndResult(SpreedlyStatus.Ok, new Invoice());

            var paidInvoice = _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                                          1111,
                                                                                          new CreditCard());

            Assert.NotNull(paidInvoice);
        }

        [Test]
        public void ChangingSubscriptionLevelWithOnFilePayment_PaysInvoiceUsingOnFile()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Created, new Invoice());
            SetPayInvoiceStatusAndResult(SpreedlyStatus.Ok, new Invoice());

            _subscriberHelper.ChangeSubscriberFeatureLevelWithOnFilePayment(subscriber, 1111);

            _paymentsClient.Received().PayInvoice(Arg.Any<Invoice>(),
                                                  Arg.Is<Payment>(p => p.AccountType == "on-file"));
        }

        [Test]
        [ExpectedException(typeof(NotFoundException))]
        public void CreateSubscription_WithNotFoundForCreate_ThrowsNotFoundException()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.NotFound, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        11111,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(UnprocessableEntityException))]
        public void CreateSubscription_WithUnprocesseableEntityForCreate_ThrowsUnprocessableEntityException()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.UnprocessableEntity, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        1111,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithForbiddenStatusForCreate_ThrowsForbiddenActionException()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Forbidden, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        1111,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(ForbiddenActionException))]
        public void CreateSubscription_WithForbiddenStatusForPay_ThrowsForbiddenActionException()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Created, new Invoice());
            SetPayInvoiceStatusAndResult(SpreedlyStatus.Forbidden, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        11111,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(UnprocessableEntityException))]
        public void CreateSubscription_WithUnprocessableEntityStatusForPay_ThrowsUnprocessableEntityException()
        {
            var subscriber = new Subscriber
            {
                CustomerId = "Id"
            };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Created, new Invoice());
            SetPayInvoiceStatusAndResult(SpreedlyStatus.UnprocessableEntity, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        11111,
                                                                        new CreditCard());
        }

        [Test]
        [ExpectedException(typeof(PaymentGatewayTimeoutException))]
        public void CreateSubscription_WithGatewayTimeoutStatusForPay_ThrowsPaymentGatewayException()
        {
            var subscriber = new Subscriber
                                 {
                                     CustomerId = "Id"
                                 };
            SetCreateInvoiceStatusAndResult(SpreedlyStatus.Created, new Invoice());
            SetPayInvoiceStatusAndResult(SpreedlyStatus.GatewayTimeout, null);

            _subscriberHelper.SubscribeToSubscriptionPlanWithCreditCard(subscriber,
                                                                        1111,
                                                                        new CreditCard());
        }

        [Test]
        public void CreateFreeTrialSubscription_ForExistingPlan_ReturnsSubscriber()
        {
            var freeTrialPlanId = 11111;
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
                                                                                                                    new SubscriptionPlan{ Id = freeTrialPlanId }
                                                                                                                }
                                                                                    }

                                                                                });
            _subscriberClient.SubscribeSubscriberToFreeTrial(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.Ok,
                                                                                                   Entity = new Subscriber()
                                                                                               });

            subscriber = _subscriberHelper.SubscribeToFreeTrialPlan(subscriber, freeTrialPlanId);

            Assert.NotNull(subscriber);
        }

        [Test]
        public void CreateFreeTrialSubscription_ForExistingPlan_PassesCorrectSubscriber()
        {
            var freeTrialPlanId = 11111;
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
                                                                                                                   new SubscriptionPlan
                                                                                                                       {
                                                                                                                           Id = freeTrialPlanId
                                                                                                                       }
                                                                                                               }
                                                                                                   }

                                                                                  });
            _subscriberClient.SubscribeSubscriberToFreeTrial(null, null).ReturnsForAnyArgs(new SpreedlyResponse
                                                                                               <Subscriber>
                                                                                               {
                                                                                                   Status = SpreedlyStatus.Ok,
                                                                                                   Entity = new Subscriber()
                                                                                               });

            _subscriberHelper.SubscribeToFreeTrialPlan(subscriber, freeTrialPlanId);

            _subscriberClient.Received().SubscribeSubscriberToFreeTrial(
                Arg.Is<string>(s => s == customerId),
                Arg.Is<SubscriptionPlan>(sp => sp.Id.Value == freeTrialPlanId));
        }

        private void SetPayInvoiceStatusAndResult(SpreedlyStatus status, Invoice invoice)
        {
            _paymentsClient.PayInvoice(null, null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                         {
                                                                             Status = status,
                                                                             Entity = invoice
                                                                         });
        }

        private void SetCreateInvoiceStatusAndResult(SpreedlyStatus status, Invoice invoice)
        {
            _paymentsClient.CreateInvoice(null).ReturnsForAnyArgs(new SpreedlyResponse<Invoice>
                                                                      {
                                                                          Status = status,
                                                                          Entity = invoice
                                                                      });
        }
    }

}