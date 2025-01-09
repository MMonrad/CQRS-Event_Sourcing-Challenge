using EventFlow;
using EventFlow.Queries;
using PMI.Domain.AccountModel;
using PMI.Domain.Commands;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.ReadModels;
using PMI.Services;

namespace PMI.Commands;

public class CommandService
{
    private readonly ICommandBus _commandBus;
    private readonly LedgerSingletonService _ledgerSingletonService;
    private readonly IQueryProcessor _queryProcessor;

    public CommandService(IQueryProcessor queryProcessor, ICommandBus commandBus,
        LedgerSingletonService ledgerSingletonService)
    {
        _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _ledgerSingletonService =
            ledgerSingletonService ?? throw new ArgumentNullException(nameof(ledgerSingletonService));
    }

    public async Task<string> CreateAccount(CancellationToken cancellationToken)
    {
        var newAccountId = AccountId.New;
        await _commandBus.PublishAsync(new CreateAccountCommand(newAccountId), cancellationToken).ConfigureAwait(false);
        return newAccountId.Value;
    }

    public async Task<string> Deposit(string id, decimal amount, CancellationToken cancellationToken)
    {
        var account =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(id), cancellationToken);
        var newEntryId = LedgerEntryId.New;
        await _commandBus
            .PublishAsync(new RegisterDepositCommand(
                    await _ledgerSingletonService.GetLedgerId(), newEntryId,
                    account.AccountId, DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
        return newEntryId.Value;
    }

    public async Task<string> Withdraw(string id, decimal amount, CancellationToken cancellationToken)
    {
        var account =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(id), cancellationToken);
        var newEntryId = LedgerEntryId.New;
        await _commandBus
            .PublishAsync(new RegisterWithdrawalCommand(
                    await _ledgerSingletonService.GetLedgerId(), newEntryId,
                    account.AccountId, DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
        return newEntryId.Value;
        ;
    }

    public async Task<string> Transfer(string from, string to, decimal amount, CancellationToken cancellationToken)
    {
        var sourceAccount =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(from), cancellationToken);
        var targetAccount =
            await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<AccountReadModel>(to), cancellationToken);
        var newEntryId = LedgerEntryId.New;
        await _commandBus
            .PublishAsync(new RegisterTransferCommand(
                    await _ledgerSingletonService.GetLedgerId(), newEntryId,
                    sourceAccount.AccountId, targetAccount.AccountId, DateTimeOffset.Now, amount),
                cancellationToken).ConfigureAwait(false);
        return newEntryId.Value;
    }
}