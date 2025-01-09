using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.LedgerModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class LedgerEntryAddedEvent : AggregateEvent<LedgerAggregate, LedgerId>
{
    public LedgerEntryAddedEvent(LedgerEntry ledgerEntry)
    {
        LedgerEntry = ledgerEntry;
    }

    public LedgerEntry LedgerEntry { get; }
}