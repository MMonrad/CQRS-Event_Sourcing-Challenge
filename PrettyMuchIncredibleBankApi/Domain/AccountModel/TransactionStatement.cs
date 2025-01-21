using PMI.Domain.TransactionModel;

namespace PMI.Domain.AccountModel;

public record TransactionStatement(string TransactionId, TransactionType TransactionType, DateTimeOffset Timestamp,
    decimal Amount){}