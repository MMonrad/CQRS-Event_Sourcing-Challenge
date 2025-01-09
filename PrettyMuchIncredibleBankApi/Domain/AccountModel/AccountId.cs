using EventFlow.Core;
using EventFlow.ValueObjects;
using Newtonsoft.Json;

namespace PMI.Domain.AccountModel;

[JsonConverter(typeof(SingleValueObjectConverter))]
public class AccountId : Identity<AccountId>
{
    public AccountId(string value) : base(value)
    {
    }
}