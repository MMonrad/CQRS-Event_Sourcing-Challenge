using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using PMI.Domain.Events;
using PMI.Domain.Specifications;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.AccountModel;

public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
{
    private readonly List<Transaction> _transactions = [];
    private decimal _balance;

    public AccountAggregate(AccountId id) : base(id)
    {
    }

    public IExecutionResult CreateAccount()
    {
        Emit(new AccountCreatedEvent());
        return ExecutionResult.Success();
    }

    public IExecutionResult Deposit(decimal amount, TransactionId transactionId, DateTimeOffset timestamp)
    {
        var transaction = new Transaction(transactionId, Id, TransactionType.Deposit, timestamp
            , amount);
        new TransactionSpecification().ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new MoneyDepositedEvent(transaction));
        return ExecutionResult.Success();
    }

    public IExecutionResult Withdraw(decimal amount, TransactionId transactionId, DateTimeOffset timestamp,
        string? transferId = null)
    {
        if (_balance - amount < 0)
        {
            return ExecutionResult.Failed("Insufficient funds");
        }

        var transaction = new Transaction(transactionId, Id, TransactionType.Credit, timestamp
            , amount);
        new TransactionSpecification().ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new MoneyWithdrawnEvent(transaction),
            transferId is not null ? new Metadata(new KeyValuePair<string, string>("transfer-id", transferId)) : null);
        return ExecutionResult.Success();
    }

    public IExecutionResult InitiateTransfer(decimal amount, AccountId destinationAccountId,
        TransactionId transactionId, DateTimeOffset timestamp)
    {
        if (_balance - amount < 0)
        {
            return ExecutionResult.Failed("Insufficient funds");
        }

        Emit(new TransferInitiatedEvent(transactionId, Id, destinationAccountId, amount, timestamp),
            new Metadata(new KeyValuePair<string, string>("transfer-id", Guid.NewGuid().ToString()))
        );
        return ExecutionResult.Success();
    }

    public void Apply(MoneyDepositedEvent moneyDepositedEvent)
    {
        _transactions.Add(moneyDepositedEvent.Transaction);
        _balance += moneyDepositedEvent.Transaction.Amount;
        Emit(new BalanceUpdatedEvent(moneyDepositedEvent.Transaction.Id, TransactionType.Deposit,
            moneyDepositedEvent.Transaction.Amount, _balance, moneyDepositedEvent.Transaction.Timestamp));
    }

    public void Apply(MoneyWithdrawnEvent moneyWithdrawnEvent)
    {
        _transactions.Add(moneyWithdrawnEvent.Transaction);
        _balance -= moneyWithdrawnEvent.Transaction.Amount;
        Emit(new BalanceUpdatedEvent(moneyWithdrawnEvent.Transaction.Id, TransactionType.Credit,
            moneyWithdrawnEvent.Transaction.Amount, _balance, moneyWithdrawnEvent.Transaction.Timestamp));
    }

    public void Apply(TransferInitiatedEvent transferInitiatedEvent)
    {
        // Handled by Saga
    }

    public void Apply(AccountCreatedEvent accountCreatedEvent)
    {
        _balance = 0;
    }

    public void Apply(BalanceUpdatedEvent balanceUpdatedEvent)
    {
        // For subscriber
    }
}