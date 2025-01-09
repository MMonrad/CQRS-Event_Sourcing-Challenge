using EventFlow;
using PMI.Domain.Commands;
using PMI.Domain.LedgerModel;

namespace PMI.Services;

public class LedgerSingletonService
{
    private readonly ICommandBus _commandBus;
    private LedgerId? _ledgerId;

    public LedgerSingletonService(ICommandBus commandBus)
    {
        _commandBus = commandBus;
    }

    public async Task<LedgerId> GetLedgerId()
    {
        if (_ledgerId is not null)
        {
            return _ledgerId;
        }

        _ledgerId = LedgerId.New;
        await _commandBus.PublishAsync(new CreateLedgerCommand(_ledgerId), CancellationToken.None);
        
        return _ledgerId!;
    }
}