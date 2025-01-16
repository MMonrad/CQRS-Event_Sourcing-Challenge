namespace PrettyMuchIncredibleBankDemo.Models;

public enum TransactionType
{
    Deposit,
    Credit
}

public record Transaction(
    string TransactionId,
    string AccountId,
    TransactionType TransactionType,
    DateTimeOffset Timestamp,
    decimal Amount)
{
}