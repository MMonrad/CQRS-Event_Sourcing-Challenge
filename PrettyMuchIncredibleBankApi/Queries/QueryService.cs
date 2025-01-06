using EventFlow.ReadStores.InMemory;
using PMI.Domain.Models;
using PMI.Domain.ReadModels;

namespace PMI.Queries;

public class QueryService
{
    private readonly IInMemoryReadStore<AccountReadModel> _accountReadStore;

    public QueryService(IInMemoryReadStore<AccountReadModel> accountReadStore)
    {
        _accountReadStore = accountReadStore ?? throw new ArgumentNullException(nameof(accountReadStore));
    }

    public async Task<Account> GetBalance(string accountId, CancellationToken cancellationToken)
    {
        var account = await _accountReadStore.GetAsync(accountId, cancellationToken).ConfigureAwait(false);
        return await account.ReadModel.ToAccount();
    }
}