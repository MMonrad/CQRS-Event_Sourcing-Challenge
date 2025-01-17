using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class MoneyWithdrawnEvent : AggregateEvent<AccountAggregate, AccountId>, ILoggableEvent
{
    public MoneyWithdrawnEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }

    public string LogMessage()
    {
        return
            $"Withdrawn {Transaction.Amount:C2} with transaction {Transaction.Id} as {Enum.GetName(typeof(TransactionType), Transaction.TransactionType)} ";
    }
}