using EventFlow.Entities;
using PMI.Domain.AccountModel;

namespace PMI.Domain.TransactionModel;

public class Transaction : Entity<TransactionId>
{
    public Transaction(TransactionId id, AccountId accountId, TransactionType transactionType, DateTimeOffset timestamp,
        decimal amount) : base(id)
    {
        AccountId = accountId;
        TransactionType = transactionType;
        Timestamp = timestamp;
        Amount = amount;
    }

    public TransactionType TransactionType { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public decimal Amount { get; set; }

    public AccountId AccountId { get; set; }
}