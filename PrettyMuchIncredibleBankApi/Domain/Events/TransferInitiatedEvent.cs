using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Events;

public class TransferInitiatedEvent : AggregateEvent<AccountAggregate, AccountId>, ILoggableEvent
{
    public TransactionId TransactionId { get; }
    public AccountId SourceAccountId { get; }
    public AccountId TargetAccountId { get; }
    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    
    public TransferInitiatedEvent(TransactionId transactionId, AccountId sourceAccountId, 
        AccountId targetAccountId, decimal amount, DateTimeOffset timestamp)
    {
        TransactionId = transactionId;
        SourceAccountId = sourceAccountId;
        TargetAccountId = targetAccountId;
        Amount = amount;
        Timestamp = timestamp;
    }
    
    
    public string LogMessage()
    {
        return $"Transferred {Amount:C2} with transaction {TransactionId} between {SourceAccountId} and {TargetAccountId}";
    }
}