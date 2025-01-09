using EventFlow.Entities;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.LedgerEntryModel;

public class LedgerEntry : Entity<LedgerEntryId>
{
    public LedgerEntry(LedgerEntryId id, DateTimeOffset timestamp, decimal amount, List<Transaction> transactions) : base(id)
    {
        Timestamp = timestamp;
        Amount = amount;
        Transactions = transactions;
    }

    public DateTimeOffset Timestamp { get; set; }
    public decimal Amount { get; set; }
    public List<Transaction> Transactions { get; set; }
}