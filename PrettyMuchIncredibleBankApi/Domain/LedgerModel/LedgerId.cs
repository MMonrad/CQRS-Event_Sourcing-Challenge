using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PMI.Domain.LedgerModel;

[JsonConverter(typeof(SingleValueObjectConverter))]
public class LedgerId : Identity<LedgerId>
{
    public LedgerId(string value) : base(value)
    {
    }
}