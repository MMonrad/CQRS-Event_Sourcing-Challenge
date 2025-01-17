using EventFlow.Aggregates;
using EventFlow.Subscribers;
using Microsoft.AspNetCore.SignalR;
using PMI.Domain.Events;
using PMI.Services;

namespace PMI.Domain.Subscribers;

public class LogSubscriber : ISubscribeSynchronousToAll
{
    private readonly IHubContext<LogHub, ILogHub> _hubContext;

    public LogSubscriber(IHubContext<LogHub, ILogHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        //TODO: Organize clients into group by account ID
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            if (aggregateEvent is ILoggableEvent loggableEvent)
            {
                await _hubContext.Clients.All.SendLogEntry(
                    new Log(domainEvent.Timestamp, domainEvent.EventType.Name, loggableEvent.LogMessage()));
            }
        }
    }
}