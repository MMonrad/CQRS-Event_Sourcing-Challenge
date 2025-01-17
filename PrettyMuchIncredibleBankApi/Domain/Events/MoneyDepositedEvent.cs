using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class MoneyDepositedEvent : AggregateEvent<AccountAggregate, AccountId>, ILoggableEvent
{
    public MoneyDepositedEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }
    public string LogMessage()
    {
        return $"Deposited {Transaction.Amount:C2} with transaction {Transaction.Id} as {Enum.GetName(typeof(TransactionType), Transaction.TransactionType)} ";
    }
}