using Microsoft.AspNetCore.Mvc;
using PMI.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.LedgerModel;
using PMI.Queries;

namespace PMI.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly CommandService _commandService;
    private readonly QueryService _queryService;

    public ApiController(CommandService commandService, QueryService queryService)
    {
        _commandService = commandService;
        _queryService = queryService;
    }

    [HttpGet]
    [Route("ledger")]
    public async Task<ActionResult<LedgerStatement>> GetLedger(CancellationToken cancellationToken)
    {
        var ledger = await _queryService.GetLedger(cancellationToken);
        return ledger.ToLedgerStatement();
        return Ok(ledger);
    }

    [HttpGet]
    [Route("accounts")]
    public async Task<ActionResult<AccountStatement>> CreateAccount(CancellationToken cancellationToken)
    {
        var createdAccount = await _commandService.CreateAccount(cancellationToken);

        return Ok(createdAccount);
    }

    [HttpGet]
    [Route("accounts/{id}")]
    public async Task<ActionResult<AccountStatement>> Account([FromRoute] string id,
        CancellationToken cancellationToken)
    {
        return Ok(await GetAccount(id, cancellationToken));
    }

    [HttpGet]
    [Route("accounts/{id}/deposit/{amount}")]
    public async Task<ActionResult<AccountStatement>> Deposit([FromRoute] string id, [FromRoute] decimal amount,
        CancellationToken cancellationToken)
    {
        await _commandService.Deposit(id, amount, cancellationToken);
        return Ok(await GetAccount(id, cancellationToken));
    }

    [HttpGet]
    [Route("accounts/{id}/withdraw/{amount}")]
    public async Task<ActionResult<AccountStatement>> Withdraw([FromRoute] string id, [FromRoute] decimal amount,
        CancellationToken cancellationToken)
    {
        await _commandService.Withdraw(id, amount, cancellationToken);
        return Ok(await GetAccount(id, cancellationToken));
    }

    [HttpGet]
    [Route("accounts/{from}/transfer/{to}/{amount}")]
    public async Task<ActionResult<List<AccountStatement>>> Transfer([FromRoute] string from, [FromRoute] string to,
        [FromRoute] decimal amount, CancellationToken cancellationToken)
    {
        await _commandService.Transfer(from, to, amount, cancellationToken);
        var fromAccount = await GetAccount(from, cancellationToken);
        var toAccount = await GetAccount(to, cancellationToken);
        return Ok(new List<AccountStatement> { fromAccount, toAccount });
    }

    private async Task<AccountStatement> GetAccount(string id, CancellationToken cancellationToken)
    {
        var account = await _queryService.GetAccount(id, cancellationToken);
        return account.ToAccountStatement();
    }
}