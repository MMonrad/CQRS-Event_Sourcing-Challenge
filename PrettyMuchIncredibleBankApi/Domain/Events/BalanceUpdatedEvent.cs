using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class BalanceUpdatedEvent : AggregateEvent<AccountAggregate, AccountId>
{
    public BalanceUpdatedEvent(TransactionId transactionId, TransactionType transactionType, decimal transactionAmount,
        decimal currentBalance, DateTimeOffset timestamp)
    {
        TransactionId = transactionId;
        TransactionType = transactionType;
        TransactionAmount = transactionAmount;
        CurrentBalance = currentBalance;
        Timestamp = timestamp;
    }

    public TransactionId TransactionId { get; }
    public TransactionType TransactionType { get; }
    public decimal TransactionAmount { get; }
    public decimal CurrentBalance { get; }
    public DateTimeOffset Timestamp { get; }
}