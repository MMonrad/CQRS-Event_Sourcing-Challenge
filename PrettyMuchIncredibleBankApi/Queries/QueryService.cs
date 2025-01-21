using EventFlow.ReadStores.InMemory;
using PMI.Domain.ReadModels;

namespace PMI.Queries;

public class QueryService
{
    private readonly IInMemoryReadStore<AccountReadModel> _accountReadStore;

    public QueryService(IInMemoryReadStore<AccountReadModel> accountReadStore)
    {
        _accountReadStore = accountReadStore ?? throw new ArgumentNullException(nameof(accountReadStore));
    }

    public async Task<AccountReadModel> GetAccount(string accountId, CancellationToken cancellationToken)
    {
        var account = await _accountReadStore.GetAsync(accountId, cancellationToken).ConfigureAwait(false);
        return account.ReadModel;
    }
    public async Task<List<AccountReadModel>> GetAccounts(CancellationToken cancellationToken)
    {
        var account = await _accountReadStore.FindAsync(x => true, cancellationToken).ConfigureAwait(false);
        return account.ToList();
    }
    public async Task<List<AccountReadModel>> SearchAccounts(string? query, CancellationToken cancellationToken)
    {
        var account = await _accountReadStore.FindAsync(x => 
            query is null || x.AccountId.Value.ToLowerInvariant().Contains(query.ToLowerInvariant()) , cancellationToken).ConfigureAwait(false);
        return account.ToList();
    }
}