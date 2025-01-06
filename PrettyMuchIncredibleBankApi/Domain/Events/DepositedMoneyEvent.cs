using EventFlow.Aggregates;

namespace PMI.Domain.Events;

public class DepositedMoneyEvent : AggregateEvent<AccountAggregate, AccountId>
{
    public DepositedMoneyEvent(double amount)
    {
        Amount = amount;
    }
    
    public double Amount { get; }
    
}