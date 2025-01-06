using EventFlow.Aggregates;

namespace PMI.Domain.Events;

public class CreatedAccountEvent : AggregateEvent<AccountAggregate, AccountId>
{
}