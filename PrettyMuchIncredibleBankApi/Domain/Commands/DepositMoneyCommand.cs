using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Commands;

public class DepositMoneyCommand : Command<AccountAggregate, AccountId, IExecutionResult>
{
    public DepositMoneyCommand(AccountId id, TransactionId transactionId, DateTimeOffset timestamp, decimal amount, string? transferId = null) :
        base(id)
    {
        Amount = amount;
        Timestamp = timestamp;
        TransactionId = transactionId;
        TransferId = transferId;
    }

    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    public TransactionId TransactionId { get; }
    public string? TransferId { get; }
}

public class
    DepositMoneyCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, DepositMoneyCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate, DepositMoneyCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Deposit(command.Amount, command.TransactionId, command.Timestamp, command.TransferId));
    }
}