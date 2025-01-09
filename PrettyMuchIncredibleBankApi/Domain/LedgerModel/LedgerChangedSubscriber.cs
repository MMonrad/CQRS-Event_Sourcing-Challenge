using EventFlow.Aggregates;
using EventFlow.Jobs;
using EventFlow.Subscribers;
using PMI.Domain.Events;
using PMI.Domain.LedgerEntryModel;

namespace PMI.Domain.LedgerModel;

public class LedgerChangedSubscriber : ISubscribeSynchronousTo<LedgerAggregate, LedgerId, LedgerEntryAddedEvent>
{
    private readonly IJobScheduler _jobScheduler;

    public LedgerChangedSubscriber(IJobScheduler jobScheduler)
    {
        _jobScheduler = jobScheduler;
    }

    public Task HandleAsync(IDomainEvent<LedgerAggregate, LedgerId, LedgerEntryAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var job = new ApplyTransactionsJob(domainEvent.AggregateEvent.LedgerEntry.Transactions);
        return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
    }
}