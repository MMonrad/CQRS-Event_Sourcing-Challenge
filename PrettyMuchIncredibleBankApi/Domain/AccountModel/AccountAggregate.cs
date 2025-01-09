using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using EventFlow.Provided.Specifications;
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

    public IExecutionResult Withdraw(decimal amount, TransactionId transactionId, DateTimeOffset timestamp)
    {
        var transaction = new Transaction(transactionId, Id,TransactionType.Credit,  timestamp
            , amount);
        new TransactionSpecification()
            .And(new ExpressionSpecification<Transaction>(x => _balance - x.Amount >= 0))
            .ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new MoneyWithdrawnEvent(transaction));
        return ExecutionResult.Success();
    }

    public void Apply(MoneyDepositedEvent moneyDepositedEvent)
    {
        _transactions.Add(moneyDepositedEvent.Transaction);
        _balance += moneyDepositedEvent.Transaction.Amount;
    }

    public void Apply(MoneyWithdrawnEvent moneyWithdrawnEvent)
    {
        _transactions.Add(moneyWithdrawnEvent.Transaction);
        _balance -= moneyWithdrawnEvent.Transaction.Amount;
    }

    public void Apply(AccountCreatedEvent accountCreatedEvent)
    {
        _balance = 0;
    }
}