using EventFlow.Aggregates;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Sagas;

public class TransferSagaStartedEvent : AggregateEvent<TransferSaga, TransferSagaId>
{
    public TransferSagaStartedEvent(AccountId sourceAccountId, AccountId targetAccountId, TransactionId transactionId,
        decimal amount)
    {
        SourceAccountId = sourceAccountId;
        TargetAccountId = targetAccountId;
        TransactionId = transactionId;
        Amount = amount;
    }

    public AccountId SourceAccountId { get; set; }
    public AccountId TargetAccountId { get; set; }
    public TransactionId TransactionId { get; set; }
    public decimal Amount { get; set; }
}