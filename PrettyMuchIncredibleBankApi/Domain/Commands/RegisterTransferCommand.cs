using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.LedgerModel;

namespace PMI.Domain.Commands;

public class RegisterTransferCommand : Command<LedgerAggregate, LedgerId, IExecutionResult>
{
    public RegisterTransferCommand(LedgerId id, LedgerEntryId ledgerEntryId, AccountId sourceAccountId,
        AccountId targetAccountId, DateTimeOffset timestamp, decimal amount) :
        base(id)
    {
        Amount = amount;
        Timestamp = timestamp;
        LedgerEntryId = ledgerEntryId;
        SourceAccountId = sourceAccountId;
        TargetAccountId = targetAccountId;
    }

    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    public LedgerEntryId LedgerEntryId { get; }
    public AccountId SourceAccountId { get; }
    public AccountId TargetAccountId { get; }
}

public class
    RegisterTransferCommandHandler : CommandHandler<LedgerAggregate, LedgerId, IExecutionResult,
    RegisterTransferCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(LedgerAggregate aggregate,
        RegisterTransferCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Transfer(command.LedgerEntryId, command.SourceAccountId,
            command.TargetAccountId, command.Timestamp,
            command.Amount));
    }
}