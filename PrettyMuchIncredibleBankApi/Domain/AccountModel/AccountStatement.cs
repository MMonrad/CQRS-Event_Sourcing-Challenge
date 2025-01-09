using PMI.Domain.TransactionModel;

namespace PMI.Domain.AccountModel;

public record AccountStatement(string AccountId, decimal Balance, List<Transaction> Transactions, int Version)
{ }