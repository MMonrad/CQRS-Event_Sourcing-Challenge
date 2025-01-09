using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PMI.Domain.AccountModel;
using PMI.Domain.Events;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.LedgerModel;

namespace PMI.Domain.ReadModels;

public class LedgerReadModel : IReadModel,
    IAmReadModelFor<LedgerAggregate, LedgerId, LedgerEntryAddedEvent>,
    IAmReadModelFor<LedgerAggregate, LedgerId, LedgerCreatedEvent>
{
    public LedgerId LedgerId { get; private set; }
    public decimal Balance { get; private set; }
    public List<LedgerEntry> Entries { get; private set; }

    private int Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<LedgerAggregate, LedgerId, LedgerCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        LedgerId = (LedgerId)domainEvent.GetIdentity();
        Balance = 0;
        Entries = [];
        Version = domainEvent.AggregateSequenceNumber;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<LedgerAggregate, LedgerId, LedgerEntryAddedEvent> domainEvent, CancellationToken cancellationToken)
    {
        LedgerId = (LedgerId)domainEvent.GetIdentity();
        Entries.Add(domainEvent.AggregateEvent.LedgerEntry);
        Version = domainEvent.AggregateSequenceNumber;
        Balance += domainEvent.AggregateEvent.LedgerEntry.Amount;
        return Task.CompletedTask;
    }
    public LedgerStatement ToLedgerStatement()
    {
        return new LedgerStatement(LedgerId.Value, Balance, Entries, Version);
    }
}