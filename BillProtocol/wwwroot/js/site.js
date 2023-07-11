// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function sendXRPLPayment(seedCode, assetCode, amount, destinationAddress) {
    try {
        const originWallet = xrpl.Wallet.fromSeed(seedCode);
            $('.loadingNewPaymentDiv').css('display', 'block');
            $('.errorData').css('display', 'none');
            const client = new xrpl.Client("wss://s.altnet.rippletest.net:51233/");
            await client.connect()
            if (assetCode == 'XRP') {
                const prepared = await client.autofill({
                    "TransactionType": "Payment",
                    "Account": originWallet.address,
                    "Amount": xrpl.xrpToDrops(amount),
                    "Destination": destinationAddress
                });
                const signed = originWallet.sign(prepared);
                const tx = await client.submitAndWait(signed.tx_blob);
                if (tx.result != null && tx.result.meta != null && tx.result.meta.TransactionResult != null
                    && tx.result.meta.TransactionResult == "tesSUCCESS") {
                    $('#successfulPayment').val(true);
                    //$('#txId').val(tx.result.meta.delivered_amount);
                    console.log(tx.result.meta);
                    //$('#secretCodeForm').submit();
                }
                else {
                    $('#successfulPayment').val(false);
                }
                
            }
            else {
                var found_paths = await client.request({
                    "command": "ripple_path_find",
                    "source_account": originAddress,
                    "destination_account": destinationAddress,
                    "destination_amount": {
                        "value": amount,
                        "currency": assetCode,
                        "issuer": destinationAddress
                    }
                });
                if (found_paths != null && found_paths.result != null && found_paths.result.status == 'success') {
                    if (found_paths.result.alternatives != null && found_paths.result.alternatives.length > 0) {
                        const prepared = await client.autofill({
                            "TransactionType": "Payment",
                            "Account": originWallet.address,
                            "Paths": found_paths.result.alternatives,
                            "Amount": {
                                "currency": assetCode,
                                "value": amount,
                                "issuer": destinationAddress
                            },
                            "Destination": destinationAddress
                        });
                        const signed = originWallet.sign(prepared);
                        const tx = await client.submitAndWait(signed.tx_blob);
                        if (tx.result != null && tx.result.meta != null && tx.result.meta.TransactionResult != null
                            && tx.result.meta.TransactionResult == "tesSUCCESS") {
                            $('#successfulPayment').val(true);
                            $('#Form_amountDelivered').val(tx.result.meta.delivered_amount);
                            $('#Form_amountSent').val(tx.result.Amount);
                            $('#Form_originalAmount').val(tx.result.Amount);
                        }
                        else {
                            $('#successfulPayment').val(false);
                        }
                        $('#secretCodeForm').submit();
                    }
                    else {
                        $('.loadingNewPaymentDiv').css('display', 'none');
                        $('.noLiquidityErrorData').css('display', 'block');
                    }
                }
                else {
                    $('.loadingNewPaymentDiv').css('display', 'none');
                    $('.errorData').css('display', 'block');
                }
            }

            client.disconnect();
        
    }
    catch (err) {
        $('.loadingNewPaymentDiv').css('display', 'none');
        $('.errorData').css('display', 'block');
    }
}


async function sendDeferredPayment(seedCode, assetCode, amount, destinationAddress) {
    try {
        const originWallet = xrpl.Wallet.fromSeed(seedCode);
            $('.loadingNewPaymentDiv').css('display', 'block');
            $('.errorData').css('display', 'none');
            const client = new xrpl.Client("wss://s.altnet.rippletest.net:51233/");
            await client.connect()
            const prepared = await client.autofill({
                "TransactionType": "CheckCreate",
                "Account": originWallet.address,
                "SendMax": assetCode == 'XRP' ? xrpl.xrpToDrops(amount) : {
                    "currency": assetCode,
                    "value": amount,
                    "issuer": destinationAddress
                },
                "Destination": destinationAddress
            });
            const signed = originWallet.sign(prepared);
            const tx = await client.submitAndWait(signed.tx_blob);
            if (tx.result != null && tx.result.meta != null && tx.result.meta.TransactionResult != null
                && tx.result.meta.TransactionResult == "tesSUCCESS") {
                var checkCreated = tx.result.meta.AffectedNodes.filter(x => x.CreatedNode != null && x.CreatedNode.LedgerEntryType == 'Check');
                if (checkCreated.length == 1) {
                    $('#successfulPayment').val(true);
                    $('#Form_ExternalId').val(checkCreated[0].CreatedNode.LedgerIndex);
                    $('.loadingNewPaymentDiv').css('display', 'none');
                    $('.paymentSuccessDiv').css('display', 'block');
                    $('#deferredPaymentForm').submit();
                }
                else {
                    $('#Form_Confirmed').val(false);
                }
            }
            else {
                $('#Form_Confirmed').val(false);
            }


            client.disconnect();
        
    }
    catch (err) {
        $('.loadingNewPaymentDiv').css('display', 'none');
        $('.errorData').css('display', 'block');
    }
}



async function cashMoneyFromPayment(originAddress, network, assetCode, amount, checkId) {
    try {
        const originWallet = xrpl.Wallet.fromSeed($('#Form_Token').val());
        if (originWallet == null || originWallet.address != originAddress) {
            $('.loadingNewPaymentDiv').css('display', 'none');
            $('.paymentSuccessDiv').css('display', 'none');
            $('.errorData').css('display', 'block');
        }
        else {
            $('.loadingNewPaymentDiv').css('display', 'block');
            $('.errorData').css('display', 'none');
            const client = new xrpl.Client(network);
            await client.connect()
            const prepared = await client.autofill({
                "TransactionType": "CheckCash",
                "Account": originWallet.address,
                "Amount": assetCode == 'XRP' ? xrpl.xrpToDrops(amount) : {
                    "currency": assetCode,
                    "value": amount,
                    "issuer": originWallet.address
                },
                "CheckID": checkId
            });
            const signed = originWallet.sign(prepared);
            const tx = await client.submitAndWait(signed.tx_blob);
            if (tx.result != null && tx.result.meta != null && tx.result.meta.TransactionResult != null
                && tx.result.meta.TransactionResult == "tesSUCCESS" && tx.result.validated == true) {
                $('.loadingNewPaymentDiv').css('display', 'none');
                $('.paymentSuccessDiv').css('display', 'block');
                $('#Form_Cashed').val(true);
                $('#cashPaymentForm').submit();
            }
            else {
                $('#Form_Cashed').val(false);
            }


            client.disconnect();
        }
    }
    catch (err) {
        $('.loadingNewPaymentDiv').css('display', 'none');
        $('.errorData').css('display', 'block');
    }
}