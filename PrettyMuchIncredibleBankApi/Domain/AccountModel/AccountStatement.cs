using PMI.Domain.TransactionModel;

namespace PMI.Domain.AccountModel;

public record AccountStatement(string AccountId, decimal Balance, List<TransactionStatement> Transactions, int Version)
{ }

public record TransactionStatement(string TransactionId, TransactionType TransactionType, DateTimeOffset Timestamp,
    decimal Amount){}