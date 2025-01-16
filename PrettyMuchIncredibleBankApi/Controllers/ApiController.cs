using EventFlow.Aggregates.ExecutionResults;
using Microsoft.AspNetCore.Mvc;
using PMI.Commands;
using PMI.Domain.AccountModel;
using PMI.Domain.TransactionModel;
using PMI.Queries;
using PMI.RequestModels;

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

    [HttpPost]
    [Route("accounts")]
    public async Task<ActionResult<AccountStatement>> CreateAccount(CancellationToken cancellationToken)
    {
        var createdAccount = await _commandService.CreateAccount(cancellationToken);
        return Ok(createdAccount);
    }

    [HttpGet]
    [Route("accounts/{id}/balance")]
    public async Task<ActionResult<decimal>> AccountBalance([FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var account = await GetAccount(id, cancellationToken);
        return Ok(account.Balance);
    }

    [HttpGet]
    [Route("accounts/{id}/history")]
    public async Task<ActionResult<List<Transaction>>> AccountHistory([FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var account = await GetAccount(id, cancellationToken);
        return Ok(account.Transactions);
    }

    [HttpPost]
    [Route("accounts/deposit")]
    public async Task<ActionResult<AccountStatement>> Deposit([FromBody] DepositRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var result = await _commandService.Deposit(requestModel.AccountId, requestModel.Amount, cancellationToken);
        if (!result.IsSuccess)
        {
            return UnprocessableEntity((result as FailedExecutionResult)?.Errors);
        }

        return Ok(await GetAccount(requestModel.AccountId, cancellationToken));
    }

    [HttpPost]
    [Route("accounts/withdraw")]
    public async Task<ActionResult<AccountStatement>> Withdraw([FromBody] WithdrawalRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var result = await _commandService.Withdraw(requestModel.AccountId, requestModel.Amount, cancellationToken);
        if (!result.IsSuccess)
        {
            return UnprocessableEntity((result as FailedExecutionResult)?.Errors);
        }

        return Ok(await GetAccount(requestModel.AccountId, cancellationToken));
    }

    [HttpPost]
    [Route("accounts/transfer")]
    public async Task<ActionResult<List<AccountStatement>>> Transfer([FromBody] TransferRequestModel requestModel,
        CancellationToken cancellationToken)
    {
        var fromAccount = await GetAccount(requestModel.SourceAccountId, cancellationToken);
        var toAccount = await GetAccount(requestModel.TargetAccountId, cancellationToken);
        var result = await _commandService.Transfer(requestModel.SourceAccountId, requestModel.TargetAccountId,
            requestModel.Amount, cancellationToken);
        if (!result.IsSuccess)
        {
            return UnprocessableEntity((result as FailedExecutionResult)?.Errors);
        }

        return Ok(new List<AccountStatement> { fromAccount, toAccount });
    }

    [HttpGet]
    [Route("accounts/list")]
    public async Task<ActionResult<List<AccountStatement>>> ListAccounts(CancellationToken cancellationToken)
    {
        var accounts = await _queryService.GetAccounts(cancellationToken);
        return Ok(accounts.Select(a => a.ToAccountStatement()));
    }

    private async Task<AccountStatement> GetAccount(string id, CancellationToken cancellationToken)
    {
        var account = await _queryService.GetAccount(id, cancellationToken);
        return account.ToAccountStatement();
    }
}