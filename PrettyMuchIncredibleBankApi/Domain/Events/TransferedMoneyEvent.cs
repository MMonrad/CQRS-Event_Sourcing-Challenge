using EventFlow.Aggregates;

namespace PMI.Domain.Events;

public class TransferedMoneyEvent : AggregateEvent<AccountAggregate, AccountId>
{
    
}