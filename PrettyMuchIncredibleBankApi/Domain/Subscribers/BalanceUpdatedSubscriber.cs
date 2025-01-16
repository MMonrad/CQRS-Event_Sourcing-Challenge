using EventFlow.Aggregates;
using EventFlow.Subscribers;
using PMI.Domain.AccountModel;
using PMI.Domain.Events;
using PMI.Domain.TransactionModel;
using PMI.Services;

namespace PMI.Domain.Subscribers;

public class BalanceUpdatedSubscriber : ISubscribeSynchronousTo<AccountAggregate, AccountId, BalanceUpdatedEvent>
{
    private readonly WebhookService _webhookService;

    public BalanceUpdatedSubscriber(WebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, BalanceUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {

        var content =
            $"Your account {domainEvent.AggregateIdentity.Value} balance changed to {domainEvent.AggregateEvent.CurrentBalance}" +
            $" due to {Enum.GetName(typeof(TransactionType), domainEvent.AggregateEvent.TransactionType)} operation of {domainEvent.AggregateEvent.TransactionAmount}" +
            $" at {domainEvent.AggregateEvent.Timestamp:g}";
        await _webhookService.SendHook("Balance Update", content);
    }
}