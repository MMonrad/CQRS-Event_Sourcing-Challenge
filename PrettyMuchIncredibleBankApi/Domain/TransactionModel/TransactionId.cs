using Newtonsoft.Json;
using EventFlow.Core;
using EventFlow.ValueObjects;

namespace PMI.Domain.TransactionModel;

[JsonConverter(typeof(SingleValueObjectConverter))]
public class TransactionId : Identity<TransactionId>
{
    public TransactionId(string value) : base(value)
    {
    }
}