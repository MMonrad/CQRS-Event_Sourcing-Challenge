using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;
using PMI.Domain.AccountModel;
using PMI.Domain.Events;
using PMI.Domain.LedgerEntryModel;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.LedgerModel;

public class LedgerAggregate : AggregateRoot<LedgerAggregate, LedgerId>
{
    private readonly SortedSet<LedgerEntry> _entries = new(new LedgerEntryComparer());

    public LedgerAggregate(LedgerId id) : base(id)
    {
    }


    //TODO: add rollback ledger entry on error in transaction subscriber?

    public IExecutionResult Deposit(LedgerEntryId ledgerEntryId, AccountId accountId, DateTimeOffset timestamp,
        decimal amount)
    {
        var ledgerEntry = new LedgerEntry(ledgerEntryId, timestamp, amount, [
            new Transaction(TransactionId.New, accountId, TransactionType.Deposit, timestamp, amount)
        ]);

        //TODO: add ledger specifications
        //  new LedgerSpecification().ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new LedgerEntryAddedEvent(ledgerEntry));
        return ExecutionResult.Success();
    }

    public IExecutionResult Withdraw(LedgerEntryId ledgerEntryId, AccountId accountId, DateTimeOffset timestamp,
        decimal amount)
    {
        var ledgerEntry = new LedgerEntry(ledgerEntryId, timestamp, amount * -1, [
            new Transaction(TransactionId.New, accountId, TransactionType.Credit, timestamp, amount)
        ]);

        //TODO: add ledger specifications
        //  new LedgerSpecification().ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new LedgerEntryAddedEvent(ledgerEntry));
        return ExecutionResult.Success();
    }

    public IExecutionResult Transfer(LedgerEntryId ledgerEntryId, AccountId sourceAccountId, AccountId targetAccountId,
        DateTimeOffset timestamp,
        decimal amount)
    {
        var ledgerEntry = new LedgerEntry(ledgerEntryId, timestamp, 0, [
            new Transaction(TransactionId.New, targetAccountId, TransactionType.Deposit, timestamp, amount),
            new Transaction(TransactionId.New, sourceAccountId, TransactionType.Credit, timestamp, amount)
        ]);

        //TODO: add ledger specifications
        //  new LedgerSpecification().ThrowDomainErrorIfNotSatisfied(transaction);
        Emit(new LedgerEntryAddedEvent(ledgerEntry));
        return ExecutionResult.Success();
    }


    public void Apply(LedgerEntryAddedEvent ledgerEntryAddedEvent)
    {
        _entries.Add(ledgerEntryAddedEvent.LedgerEntry);
    }

    public IExecutionResult CreateLedger()
    {
        Emit(new LedgerCreatedEvent());
        return ExecutionResult.Success();
    }
    public void Apply(LedgerCreatedEvent ledgerCreatedEvent)
    {
    }
}