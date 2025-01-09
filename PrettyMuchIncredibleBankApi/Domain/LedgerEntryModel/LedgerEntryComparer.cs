namespace PMI.Domain.LedgerEntryModel;

public class LedgerEntryComparer : IComparer<LedgerEntry>
{
    public int Compare(LedgerEntry? x, LedgerEntry? y)
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