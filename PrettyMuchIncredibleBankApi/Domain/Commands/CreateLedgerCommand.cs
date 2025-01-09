
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Extensions;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerModel;

namespace PMI.Domain.Commands;

public class CreateLedgerCommand : DistinctCommand<LedgerAggregate, LedgerId, IExecutionResult>
{
    public CreateLedgerCommand(LedgerId id) : base(id)
    {
        NewId = id;
    }

    public LedgerId NewId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return NewId.GetBytes();
    }
}

public class CreateLedgerCommandHandler : CommandHandler<LedgerAggregate, LedgerId, IExecutionResult, CreateLedgerCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(LedgerAggregate aggregate, CreateLedgerCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.CreateLedger());
    }
}