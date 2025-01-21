using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Commands;

public class RegisterWithdrawalCommand : Command<AccountAggregate, AccountId, IExecutionResult>
{
    public RegisterWithdrawalCommand(AccountId id, TransactionId transactionId, DateTimeOffset timestamp, decimal amount) :
        base(id)
    {
        Amount = amount;
        Timestamp = timestamp;
        TransactionId = transactionId;
    }

    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    public TransactionId TransactionId { get; }
}

public class
    RegisterWithdrawalCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult,
    RegisterWithdrawalCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
        RegisterWithdrawalCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Withdraw(command.Amount, command.TransactionId, command.Timestamp));
    }
}