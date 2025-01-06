using EventFlow;
using EventFlow.Queries;
using PMI.Domain;
using PMI.Domain.Commands;

namespace PMI.Commands;

public class CommandService
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryProcessor _queryProcessor;

    public CommandService(IQueryProcessor queryProcessor, ICommandBus commandBus)
    {
        _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }

    public async Task<string> CreateAccount(CancellationToken cancellationToken)
    {
        var newAccountId = AccountId.New;
        await _commandBus.PublishAsync(new CreateAccountCommand(newAccountId), cancellationToken).ConfigureAwait(false);
        return newAccountId.Value;
    }
}