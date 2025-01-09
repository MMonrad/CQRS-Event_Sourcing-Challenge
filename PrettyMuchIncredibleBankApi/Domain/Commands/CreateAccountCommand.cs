
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using EventFlow.Extensions;
using PMI.Domain.AccountModel;

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

public class CreateAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, CreateAccountCommand>
{
    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate, CreateAccountCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.CreateAccount());
    }
}