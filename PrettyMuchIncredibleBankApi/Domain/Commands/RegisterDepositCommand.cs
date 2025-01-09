using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.LedgerModel;

namespace PMI.Domain.Commands;

public class RegisterDepositCommand : Command<LedgerAggregate, LedgerId, IExecutionResult>
{
    public RegisterDepositCommand(LedgerId id, LedgerEntryId ledgerEntryId, AccountId accountId,
        DateTimeOffset timestamp, decimal amount) :
        base(id)
    {
        Amount = amount;
        Timestamp = timestamp;
        LedgerEntryId = ledgerEntryId;
        AccountId = accountId;
    }

    public decimal Amount { get; }
    public DateTimeOffset Timestamp { get; }
    public LedgerEntryId LedgerEntryId { get; }
    public AccountId AccountId { get; }
}

public class
    RegisterDepositCommandHandler : CommandHandler<LedgerAggregate, LedgerId, IExecutionResult, RegisterDepositCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(LedgerAggregate aggregate,
        RegisterDepositCommand command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.Deposit(command.LedgerEntryId, command.AccountId, command.Timestamp,
            command.Amount));
    }
}