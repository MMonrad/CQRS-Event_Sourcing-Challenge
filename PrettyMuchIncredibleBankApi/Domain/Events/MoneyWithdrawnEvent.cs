using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class MoneyWithdrawnEvent : AggregateEvent<AccountAggregate, AccountId>
{
    public MoneyWithdrawnEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }
}