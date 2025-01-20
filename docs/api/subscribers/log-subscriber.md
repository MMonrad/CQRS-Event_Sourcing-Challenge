---
icon: blog
---

# Log Subscriber

Subscribes to all of the Events that impement the `ILoggableEvent` intefrace, namely:

* [AccountCreated ](../events/accountcreated.md)Event
* [MoneyDeposited ](../events/moneydeposited.md)Event
* [MoneyWithdrawn ](../events/moneywithdrawn.md)Event
* [TransferInitiated ](../events/transferinitiated.md)Event
* [TransferSagaStarted ](../events/transfersagastarted.md)Event

Invoked the [Log Hub](../services/log-hub.md) to send a log message.
