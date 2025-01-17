using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Commands;

public class InitiateTransferCommand : Command<AccountAggregate, AccountId, IExecutionResult>
{
    public InitiateTransferCommand(TransactionId transactionId, AccountId id,
        AccountId targetAccountId, DateTimeOffset timestamp, decimal amount) :
        base(id)
    {
        Amount = amount;
        Timestamp = timestamp;
        TargetAccountId = targetAccountId;
        TransactionId = transactionId;
    }

    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    public AccountId TargetAccountId { get; }
    public TransactionId TransactionId { get; }
}

public class
    RegisterTransferCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult,
    InitiateTransferCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate,
        InitiateTransferCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.InitiateTransfer(command.Amount, command.TargetAccountId, command.TransactionId,
            command.Timestamp));
    }
}