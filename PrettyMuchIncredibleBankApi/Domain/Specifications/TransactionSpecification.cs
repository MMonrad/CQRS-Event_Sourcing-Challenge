using EventFlow.Specifications;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.Specifications;

public class TransactionSpecification : Specification<Transaction>
{
    protected override IEnumerable<string> IsNotSatisfiedBecause(Transaction transaction)
    {
                if (transaction.Amount <= 0)
                {
                    yield return "Amount must be greater than 0";
                }
    }
}