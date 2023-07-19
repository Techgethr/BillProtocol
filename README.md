# Bill Protocol

Bill Protocol is a web platform that allows you to manage invoices and payments on the [XRPL network](https://xrpl.org/), with the idea of simplifying money collection processes through blockchain.

_Bill Protocol currently operates on the XRPL testnet._

### Demo
https://www.youtube.com/watch?v=gAN1A21-NfI 

## Options

With Bill Protocol, you can:
- Generate and receive invoices: You can create several types of invoices to your recipients and enter the payment items. You can approve and reject the invoices received, and pay them when appropriate.
- Manage recipients: To send invoices.
- Manage wallets: Where you want to receive the money (without storing the seed code).
- Manage information: About your person or company, which is used to (in the future) print PDF with the invoice with all the information about its creator.

## Invoice types

The types of invoices available are:
- Regular: The recipient of the invoice can pay it immediately. **Use the regular XRPL payment process.**
- Payment to Date: The recipient approves the invoice (a check is generated) and the invoice creator can redeem the money on the specified payment date, as long as the invoice recipient has a balance in their account. **Use XRPL checks.**
- Escrows: The receiver approves the invoice (an escrow is generated, where this user must deposit the money) and the invoice creator can redeem the money on the specified payment date since the amount is already available on the network. **Use XRPL escrows.**

## Technologies

Bill Protocol is built with:
- [XRPL Blockchain](https://xrpl.org/)
- .NET 6
- SQL Server
- Javascript
