using EventFlow.Aggregates;
using EventFlow.ReadStores;
using PMI.Domain.Events;
using PMI.Domain.Models;

namespace PMI.Domain.ReadModels;

public class AccountReadModel : IReadModel,
    IAmReadModelFor<AccountAggregate, AccountId, CreatedAccountEvent>,
    IAmReadModelFor<AccountAggregate, AccountId, DepositedMoneyEvent>
{
    public string AccountId { get; private set; }
    public double Balance { get; private set; }

    private int Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<AccountAggregate, AccountId, CreatedAccountEvent> domainEvent, CancellationToken cancellationToken)
    {
        AccountId = domainEvent.GetIdentity().Value;
        Balance = 0;
        Version = domainEvent.AggregateSequenceNumber;
        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<AccountAggregate, AccountId, DepositedMoneyEvent> domainEvent, CancellationToken cancellationToken)
    {
        AccountId = domainEvent.GetIdentity().Value;
        Balance = domainEvent.AggregateEvent.Amount;
        Version = domainEvent.AggregateSequenceNumber;
        return Task.CompletedTask;
    }

    public Task<Account> ToAccount()
    {
        return Task.FromResult(new Account(AccountId, Version, Balance));
    }
}