using EventFlow.Aggregates;
using EventFlow.Sagas;

namespace PMI.Domain.Sagas;

public class TransferSagaLocator : ISagaLocator
{
    public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return Task.FromResult<ISagaId>(domainEvent.Metadata.TryGetValue("transfer_id", out var transferId)
            ? new TransferSagaId($"transfersaga-{transferId}")
            : TransferSagaId.New);
    }
}