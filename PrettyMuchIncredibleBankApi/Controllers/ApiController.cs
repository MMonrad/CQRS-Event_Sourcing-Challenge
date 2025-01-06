using Microsoft.AspNetCore.Mvc;
using PMI.Commands;
using PMI.Domain.Models;
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
    [Route("accounts")]
    public async Task<ActionResult<string>> CreateAccount(CancellationToken cancellationToken)
    {
        var createdAccount = await _commandService.CreateAccount(cancellationToken);

        return Ok(createdAccount);
    }

    [HttpGet]
    [Route("accounts/{id}")]
    public async Task<ActionResult<Account>> GetAccount([FromRoute] string id, CancellationToken cancellationToken)
    {
        return Ok(await _queryService.GetBalance(id, cancellationToken));
    }
}