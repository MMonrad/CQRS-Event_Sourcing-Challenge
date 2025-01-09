using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Commands;

public class WithdrawMoneyCommand : Command<AccountAggregate, AccountId, IExecutionResult>
{
    public WithdrawMoneyCommand(AccountId id, TransactionId transactionId, DateTimeOffset timestamp, decimal amount) :
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
    WithdrawMoneyCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, WithdrawMoneyCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate, WithdrawMoneyCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Withdraw(command.Amount, command.TransactionId, command.Timestamp));
    }
}