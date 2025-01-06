
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Extensions;

namespace PMI.Domain.Commands;

public class CreateAccountCommand : DistinctCommand<AccountAggregate, AccountId, IExecutionResult>
{
    public CreateAccountCommand(AccountId id) : base(id)
    {
        NewId = id;
    }

    public AccountId NewId { get; }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return NewId.GetBytes();
    }
}