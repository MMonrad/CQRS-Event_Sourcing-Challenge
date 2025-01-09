using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerModel;

namespace PMI.Domain.Events;

public class LedgerCreatedEvent : AggregateEvent<LedgerAggregate, LedgerId>
{
}