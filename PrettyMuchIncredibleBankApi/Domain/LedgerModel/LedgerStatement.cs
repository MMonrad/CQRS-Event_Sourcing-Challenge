using PMI.Domain.LedgerEntryModel;

namespace PMI.Domain.LedgerModel;

public record LedgerStatement(string LedgerId, decimal Balance, List<LedgerEntry> Entries, int Version)
{
}