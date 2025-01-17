using EventFlow.Aggregates;
using EventFlow.Jobs;
using EventFlow.Subscribers;
using PMI.Domain.AccountModel;
using PMI.Domain.Events;
using PMI.Services;

namespace PMI.Domain.Subscribers;

public class BalanceUpdatedSubscriber : ISubscribeSynchronousTo<AccountAggregate, AccountId, MoneyDepositedEvent>,
    ISubscribeSynchronousTo<AccountAggregate, AccountId, MoneyWithdrawnEvent>
{
    private readonly IJobScheduler _jobScheduler;

    public BalanceUpdatedSubscriber(WebhookService webhookService, IJobScheduler jobScheduler)
    {
        _jobScheduler = jobScheduler;
    }

    public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, MoneyDepositedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await IssueJob(domainEvent.AggregateIdentity.Value, cancellationToken);
    }

    public async Task HandleAsync(IDomainEvent<AccountAggregate, AccountId, MoneyWithdrawnEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await IssueJob(domainEvent.AggregateIdentity.Value, cancellationToken);
    }

    private async Task IssueJob(string accountId, CancellationToken cancellationToken)
    {
        var job = new SendWebhookJob(accountId, "Balance Update");
        await _jobScheduler.ScheduleAsync(job, TimeSpan.FromSeconds(5), cancellationToken);
    }
}