﻿using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PreAuthAndCollectionTests
    {
        [Test]
        public void ASimplePreAuth()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);
        }

        [Test]
        public void ADeclinedCardPreAuth()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4221690000004963",
                CV2 = "125",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public void ADeclinedValidationOnCardPreAuth()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4221690000004963",
                CV2 = "125",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Validate(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Your good to go!", response.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, response.Response.ErrorType);
        }

        [Test]
        public void ASimplePreAuthAndCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            var collection = new CollectionModel
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);
        }

        [Test]
        public void ASimplePreAuthAndValidateCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            var collection = new CollectionModel
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual("Your good to go!", validateResponse.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, validateResponse.Response.ErrorType);
        }

        [Test]
        public void AFailedSimplePreAuthAndValidateCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            var collection = new CollectionModel
            {
                Amount = 30,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsTrue(validateResponse.HasError);
            Assert.AreEqual("Unable to process collection as total amount collected would exceed value of" +
                            " original PreAuth transaction.", validateResponse.Error.ErrorMessage);
            Assert.AreEqual(JudoApiError.Payment_Failed, validateResponse.Error.ErrorType);
        }
    }
}
