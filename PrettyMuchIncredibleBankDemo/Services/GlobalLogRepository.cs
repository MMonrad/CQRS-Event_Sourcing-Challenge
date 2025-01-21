using System.Collections.ObjectModel;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using PrettyMuchIncredibleBankDemo.Models;

namespace PrettyMuchIncredibleBankDemo.Services;

public class GlobalLogRepository
{
    private readonly HubConnection _globalLogHub;

    public GlobalLogRepository(IOptions<PmiOptions> pmiOptions)
    {
        _globalLogHub = new HubConnectionBuilder().WithUrl($"{pmiOptions.Value.ApiUrl}/logs").Build();
        _globalLogHub.On<Log>("SendLogEntry", msg => { Logs.Add(msg); });
    }

    public ObservableCollection<Log> Logs { get; } = [];

    public async Task InitializeAsync()
    {
        if (_globalLogHub.State == HubConnectionState.Disconnected)
        {
            await _globalLogHub.StartAsync().ConfigureAwait(false);
        }
    }
}