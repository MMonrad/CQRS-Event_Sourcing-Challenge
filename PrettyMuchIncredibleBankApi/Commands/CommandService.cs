using EventFlow;
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Queries;
using PMI.Domain.AccountModel;
using PMI.Domain.Commands;
using PMI.Domain.ReadModels;
using PMI.Domain.TransactionModel;

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
        var result = await _commandBus.PublishAsync(new CreateAccountCommand(newAccountId), cancellationToken)
            .ConfigureAwait(false);
        return result is SuccessExecutionResult && result.IsSuccess
            ? newAccountId.Value
            : string.Join(Environment.NewLine, (result as FailedExecutionResult)!.Errors);
    }

    public async Task<IExecutionResult> Deposit(string id, decimal amount, CancellationToken cancellationToken)
    {
        var account =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(id), cancellationToken);
        var newTransactionId = TransactionId.New;
        return await _commandBus
            .PublishAsync(new RegisterDepositCommand(account.AccountId, newTransactionId, DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
    }

    public async Task<IExecutionResult> Withdraw(string id, decimal amount, CancellationToken cancellationToken)
    {
        var account =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(id), cancellationToken);
        var newTransactionId = TransactionId.New;
        return await _commandBus
            .PublishAsync(
                new RegisterWithdrawalCommand(account.AccountId, newTransactionId, DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
    }

    public async Task<IExecutionResult> Transfer(string from, string to, decimal amount,
        CancellationToken cancellationToken)
    {
        var sourceAccount =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(from), cancellationToken);
        var targetAccount =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(to), cancellationToken);
        var newTransactionId = TransactionId.New;
        return await _commandBus
            .PublishAsync(
                new InitiateTransferCommand(newTransactionId, sourceAccount.AccountId, targetAccount.AccountId,
                    DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
    }
}