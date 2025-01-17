namespace PrettyMuchIncredibleBankDemo.Models;

public record AccountStatement(string AccountId, decimal Balance, List<Transaction> Transactions, int Version)
{ }