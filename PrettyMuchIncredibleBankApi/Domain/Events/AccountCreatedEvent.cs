using EventFlow.Aggregates;
using PMI.Domain.AccountModel;

namespace PMI.Domain.Events;

public class AccountCreatedEvent : AggregateEvent<AccountAggregate, AccountId>
{
}