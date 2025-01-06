using EventFlow.Specifications;

namespace PMI.Domain.Specifications;

public class DepositSpecification : Specification<double>
{
    protected override IEnumerable<string> IsNotSatisfiedBecause(double amount)
    {
        if (amount <= 0)
        {
            yield return "Deposit amount must be greater than 0";
        }
    }
}