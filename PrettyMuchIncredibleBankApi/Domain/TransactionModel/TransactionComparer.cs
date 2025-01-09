namespace PMI.Domain.TransactionModel;

public class TransactionComparer : IComparer<Transaction>
{
    public int Compare(Transaction? x, Transaction? y)
    {
        if (ReferenceEquals(x, y))
        {
            return 0;
        }

        if (y is null)
        {
            return 1;
        }

        if (x is null)
        {
            return -1;
        }

        return x.Id.CompareTo(y.Id);
    }
}