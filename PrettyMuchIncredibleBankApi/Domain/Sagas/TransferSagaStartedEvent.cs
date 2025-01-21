using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.Events;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Sagas;

public class TransferSagaStartedEvent : AggregateEvent<TransferSaga, TransferSagaId>, ILoggableEvent
{
    public TransferSagaStartedEvent(TransferSagaId id, AccountId sourceAccountId, AccountId targetAccountId, TransactionId transactionId,
        decimal amount)
    {
        SourceAccountId = sourceAccountId;
        TargetAccountId = targetAccountId;
        TransactionId = transactionId;
        Amount = amount;
        Id = id;
    }

    public AccountId SourceAccountId { get; set; }
    public AccountId TargetAccountId { get; set; }
    public TransactionId TransactionId { get; set; }
    public TransferSagaId Id { get; set; }
    public decimal Amount { get; set; }
    public string LogMessage()
    {
        return $"Transfer Saga {Id} started for transfer event:  Transferred {Amount:C2} with transaction {TransactionId} between {SourceAccountId} and {TargetAccountId}";
    }
}