using EventFlow.ReadStores.InMemory;
using PMI.Domain.ReadModels;
using PMI.Services;

namespace PMI.Queries;

public class QueryService
{
    private readonly IInMemoryReadStore<AccountReadModel> _accountReadStore;
    private readonly IInMemoryReadStore<LedgerReadModel> _ledgerReadStore;
    private readonly LedgerSingletonService _ledgerSingletonService;

    public QueryService(IInMemoryReadStore<AccountReadModel> accountReadStore, LedgerSingletonService ledgerSingletonService, IInMemoryReadStore<LedgerReadModel> ledgerReadStore)
    {
        _accountReadStore = accountReadStore ?? throw new ArgumentNullException(nameof(accountReadStore));
        _ledgerSingletonService = ledgerSingletonService ?? throw new ArgumentNullException(nameof(ledgerSingletonService));
        _ledgerReadStore = ledgerReadStore ?? throw new ArgumentNullException(nameof(ledgerReadStore));
    }

    public async Task<AccountReadModel> GetAccount(string accountId, CancellationToken cancellationToken)
    {
        var account = await _accountReadStore.GetAsync(accountId, cancellationToken).ConfigureAwait(false);
        return account.ReadModel;
    }

    public async Task<LedgerReadModel> GetLedger(CancellationToken cancellationToken)
    {
        var ledgerId = await _ledgerSingletonService.GetLedgerId();
        var ledger = await _ledgerReadStore.GetAsync(ledgerId.Value, cancellationToken).ConfigureAwait(false);
        return ledger.ReadModel;
    }
}