using EventFlow;
using EventFlow.Jobs;
using EventFlow.Queries;
using PMI.Domain.Commands;
using PMI.Domain.TransactionModel;

namespace PMI.Domain.LedgerEntryModel;

[JobVersion("ApplyTransactionsJob", 1)]
public class ApplyTransactionsJob : IJob
{
    public ApplyTransactionsJob(List<Transaction> transactions)
    {
        Transactions = transactions;
    }

    public List<Transaction> Transactions { get; set; }


    public async Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var queryProcessor = serviceProvider.GetRequiredService<IQueryProcessor>();
        var commandBus = serviceProvider.GetRequiredService<ICommandBus>();

        foreach (var transaction in Transactions)
        {
            switch (transaction.TransactionType)
            {
                case TransactionType.Credit:
                    await commandBus.PublishAsync(
                        new WithdrawMoneyCommand(transaction.AccountId, transaction.Id, transaction.Timestamp,
                            transaction.Amount), cancellationToken);
                    break;
                case TransactionType.Deposit:
                    await commandBus.PublishAsync(
                        new DepositMoneyCommand(transaction.AccountId, transaction.Id, transaction.Timestamp,
                            transaction.Amount), cancellationToken);
                    break;
            }
        }
        //TODO Verify transaction validity etc.
    }
}