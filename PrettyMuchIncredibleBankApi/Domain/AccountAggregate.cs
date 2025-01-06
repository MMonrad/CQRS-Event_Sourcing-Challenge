using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Extensions;
using PMI.Domain.Events;
using PMI.Domain.Specifications;

namespace PMI.Domain;

public class AccountAggregate : AggregateRoot<AccountAggregate, AccountId>
{
    private double _balance;
    
    public AccountAggregate(AccountId id) : base(id) { }

    public IExecutionResult CreateAccount()
    {
        Emit(new CreatedAccountEvent());
        return ExecutionResult.Success();
    }
    
    public IExecutionResult Deposit(double amount)
    {
        Emit(new DepositedMoneyEvent(amount));
        return ExecutionResult.Success();
    }

    public void Apply(DepositedMoneyEvent depositedEvent)
    {
        new DepositSpecification().ThrowDomainErrorIfNotSatisfied(depositedEvent.Amount);
        _balance += depositedEvent.Amount;
    }
    public void Apply(CreatedAccountEvent createdAccountEvent)
    {
    }
}