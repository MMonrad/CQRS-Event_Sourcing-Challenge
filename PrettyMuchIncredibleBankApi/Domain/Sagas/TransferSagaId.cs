using EventFlow.Core;
using EventFlow.Sagas;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PMI.Domain.Sagas;

[JsonConverter(typeof(SingleValueObjectConverter))]
public class TransferSagaId : Identity<TransferSagaId>, ISagaId
{
    public TransferSagaId(string value) : base(value)
    {
    }
}