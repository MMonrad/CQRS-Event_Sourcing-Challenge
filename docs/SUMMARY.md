# Table of contents

* [Pretty Much Incredible Bank](README.md)

## API

* [Introduction](api/introduction.md)
* [Domain Model](api/domain-model/README.md)
  * [AccountAggregate](api/domain-model/accountaggregate.md)
  * [Transaction](api/domain-model/transaction.md)
  * [AccountReadModel](api/domain-model/accountreadmodel.md)
* [Persistence](api/persistence.md)
* [Commands](api/commands/README.md)
  * [CreateAccount](api/commands/createaccount.md)
  * [RegisterDeposit](api/commands/registerdeposit.md)
  * [RegisterWithdrawal](api/commands/registerwithdrawal.md)
  * [InitiateTransfer](api/commands/initiatetransfer.md)
  * [DepositMoney](api/commands/depositmoney.md)
  * [WithdrawMoney](api/commands/withdrawmoney.md)
* [Events](api/events/README.md)
  * [AccountCreated](api/events/accountcreated.md)
  * [MoneyDeposited](api/events/moneydeposited.md)
  * [MoneyWithdrawn](api/events/moneywithdrawn.md)
  * [TransferInitiated](api/events/transferinitiated.md)
  * [TransferSagaStarted](api/events/transfersagastarted.md)
* [Transfer Saga](api/transfer-saga.md)
* [Subscribers](api/subscribers/README.md)
  * [Balance Updated Subscriber](api/subscribers/balance-updated-subscriber.md)
  * [Log Subscriber](api/subscribers/log-subscriber.md)
* [Services](api/services/README.md)
  * [Webhook Service](api/services/webhook-service.md)
  * [Log Hub](api/services/log-hub.md)

## Blazor App

* [Page 2](blazor-app/page-2.md)
