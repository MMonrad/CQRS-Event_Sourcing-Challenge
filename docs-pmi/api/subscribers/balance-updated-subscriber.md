---
icon: scale-balanced
---

# Balance Updated Subscriber

This subscriber is invoked when [**MoneyDeposited** ](../events/moneydeposited.md)and [**MoneyWithdrawn**](../events/moneywithdrawn.md) events are emitted.

This subscriber schedules a synchronous Job (which with default EventFlow scheduler means it will be executed immediately) called **SendWebhookJob**.



### SendWebhook Job

Invokes the [Webhook Service](../services/webhook-service.md) to issue a message about the updated balance.
