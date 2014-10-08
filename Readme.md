# JudoPay SDK for .net 4

The JudoPay SDK is a client for the JudoPay API, which provides card payment processing 
for mobile apps and websites. This repository only contains the .net 4 library, not the PCLs.
As much as possible it is identical to the official JudoPay .net SDK [https://www.judopay.com/docs](https://www.judopay.com/docs).

## Configuration

You configure the JudoPay API client when invoking the JudoPaymentsFactory.Create method. This has
three parameters; environment (Sandbox for development and testing, and Live for production), and api
token and secret. You set you API token and secret up through the [management dashboard](https://portal.judopay.com)
after creating an account. You can create a testing account by clicking "Getting Started" in the [documentation](https://www.judopay.com/docs)

```c#
var client = JudoPaymentsFactory.Create(JudoPayDotNet.Enums.Environment.Sandbox, "YOUR_API_TOKEN", "YOUR_API_SECRET");
```

## Usage - Process a payment
Once you have your API client, you can easily process a payment:

```c#
var cardPaymentModel = new CardPaymentModel
{
	//the value of the payment
	Amount = 1.01m,
	Currency = "GBP",

	// the card details
	CardNumber = "4976000000000036",
	ExpiryDate = "1215",
	CV2 = "452",

	// identify the recipient
	JudoId = "500017",

	// provide an identifier for your customer
	YourConsumerReference = "MyCustomer004",

	// provide an identifier for this payment
	YourPaymentReference = "Payment523515",
};

client.Payments.Create(cardPaymentModel).ContinueWith(result =>
{
	var paymentResult = result.Result;

	if (paymentResult.Response.Result == "Success")
	{
		Console.WriteLine("Payment successful. Transaction Reference {0}", paymentResult.Response.ReceiptId);
	}

});
```

## Usage - List transactions

You also have access to a complete feed of all transactions within your account:

```c#
client.Transactions.Get().ContinueWith(result =>
{

	if (!result.Result.HasError)
	{
		foreach (var tx in recentTx.Result.Response.Results)
		{
			Console.WriteLine("{0} {1} {2}", tx.ReceiptId, tx.Type, tx.Amount);
		}
	}
	else
	{
		Console.WriteLine("Call returned error. {0}", result.Result.Error.ErrorMessage);
	}
});
```

