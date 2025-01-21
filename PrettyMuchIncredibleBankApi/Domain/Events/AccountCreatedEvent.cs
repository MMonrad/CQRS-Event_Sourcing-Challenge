using EventFlow.Aggregates;
using PMI.Domain.AccountModel;

namespace PMI.Domain.Events;

public class AccountCreatedEvent : AggregateEvent<AccountAggregate, AccountId>, ILoggableEvent
{
    public string LogMessage()
    {
        return $"Account created";
    }
}