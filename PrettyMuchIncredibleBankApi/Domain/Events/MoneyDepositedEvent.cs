using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class MoneyDepositedEvent : AggregateEvent<AccountAggregate, AccountId>
{
    public MoneyDepositedEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }
}