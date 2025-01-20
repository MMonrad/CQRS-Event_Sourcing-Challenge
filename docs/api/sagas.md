---
description: >-
  For the Transfer between accounts operation, as it involves bounded contexts
  of two account aggregates, a Saga is used to coordinate messages. The
  following diagram illustrated the flow of operations.
icon: books
---

# Sagas

<figure><picture><source srcset="../.gitbook/assets/transfer-event-dm.jpg" media="(prefers-color-scheme: dark)"><img src="../.gitbook/assets/transfer-event.jpg" alt=""></picture><figcaption><p>Transfer Event Flow</p></figcaption></figure>

1. User initiates the transfer between two accounts
2. A **TransferInitiated** Event is emitted from Account Aggregate, handled by **TransferSaga**, which:
   1. Emits **TransferSagaStarted Event** which stores the saga information in Saga Aggregate
   2. Published **WithdrawMoney** Command for source Account
3. Withdrawal command results in **MoneyWithdrawn** Event being emitted by the account aggregate, which:
   1. Gets picked up by the **TransferSaga** and connected with the rest of the processing paradigm
   2. Transfer Saga published **DepositMoney** command to target Account.
4. **MoneyDeposited** event is issued&#x20;
5. **TransferSaga** handles **MoneyDeposited** Event and **completes.**
