using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;
using PMI.Domain;
using PMI.Domain.Commands;

namespace PMI.Commands.CommandHandlers;

public class CreateAccountCommandHandler : CommandHandler<AccountAggregate, AccountId, IExecutionResult, CreateAccountCommand>
{

    public override Task<IExecutionResult> ExecuteCommandAsync(AccountAggregate aggregate, CreateAccountCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(aggregate.CreateAccount());
    }
}