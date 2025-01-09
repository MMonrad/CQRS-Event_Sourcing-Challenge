using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PMI.Domain.LedgerEntryModel;

[JsonConverter(typeof(SingleValueObjectConverter))]
public class LedgerEntryId : Identity<LedgerEntryId>
{
    public LedgerEntryId(string value) : base(value)
    {
    }
}