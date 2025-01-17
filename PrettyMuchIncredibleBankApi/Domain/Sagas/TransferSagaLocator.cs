using EventFlow.Aggregates;
using EventFlow.Sagas;

namespace PMI.Domain.Sagas;

public class TransferSagaLocator : ISagaLocator
{
    public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        domainEvent.Metadata.TryGetValue("transfer_id", out var transferId);
        return Task.FromResult<ISagaId>(transferId is not null
            ? TransferSagaId.With(new Guid(transferId))
            : TransferSagaId.New);
    }
}