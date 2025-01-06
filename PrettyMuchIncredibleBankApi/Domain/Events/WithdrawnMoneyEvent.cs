using EventFlow.Aggregates;

namespace PMI.Domain.Events;

public class WithdrawnMoneyEvent : AggregateEvent<AccountAggregate, AccountId>
{
    
}