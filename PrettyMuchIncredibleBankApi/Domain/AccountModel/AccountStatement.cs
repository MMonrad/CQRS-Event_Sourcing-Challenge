namespace PMI.Domain.AccountModel;

public record AccountStatement(string AccountId, decimal Balance, List<TransactionStatement> Transactions)
{ }