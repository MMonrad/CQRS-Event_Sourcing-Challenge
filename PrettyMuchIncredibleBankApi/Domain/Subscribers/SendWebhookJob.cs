using EventFlow.Jobs;
using EventFlow.Queries;
using PMI.Domain.TransactionModel;
using PMI.Queries;
using PMI.Services;

namespace PMI.Domain.Subscribers;

[JobVersion(nameof(SendWebhookJob), 1)]
public class SendWebhookJob : IJob
{
    public SendWebhookJob(string accountId, string title)
    {
        AccountId = accountId;
        Title = title;
    }

    public string AccountId { get; set; }
    public string Title { get; set; }

    public async Task ExecuteAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var webhookService = serviceProvider.GetRequiredService<WebhookService>();
        var queryService = serviceProvider.GetRequiredService<QueryService>();

        var account = await queryService.GetAccount(AccountId, cancellationToken).ConfigureAwait(false);

        var content =
            $"Your account balance changed to {account.Balance}" +
            $" due to {Enum.GetName(typeof(TransactionType), account.Transactions.Last().TransactionType)} operation of {account.Transactions.Last().Amount}" +
            $" at {account.Transactions.Last().Timestamp:g}";

        await webhookService.SendHook(Title, content);
    }
}