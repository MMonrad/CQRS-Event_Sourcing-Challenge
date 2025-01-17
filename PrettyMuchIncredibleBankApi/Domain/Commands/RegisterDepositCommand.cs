using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Commands;

public class RegisterDepositCommand : Command<AccountAggregate, AccountId, IExecutionResult>
{
    public RegisterDepositCommand(AccountId id, TransactionId transactionId,
        DateTimeOffset timestamp, decimal amount) :
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
    RegisterDepositCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult,
    RegisterDepositCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
        RegisterDepositCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Deposit(command.Amount, command.TransactionId, command.Timestamp));
    }
}