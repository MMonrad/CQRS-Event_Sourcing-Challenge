using Microsoft.AspNetCore.SignalR;

namespace PMI.Services;

public sealed class LogHub : Hub<ILogHub>
{
    public async Task SendLogEntry(Log logEntry)
    {
        await Clients.All.SendLogEntry(logEntry);
    }
    
}

public interface ILogHub
{
    Task SendLogEntry(Log logEntry);
}