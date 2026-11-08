using Lab5.Application.Contracts.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountApplicationService _accountService;

    public AccountsController(IAccountApplicationService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public IActionResult CreateAccount([FromBody] CreateAccountRequest request)
    {
        CreateAccountResult result = _accountService.CreateAccount(
            request.SessionKey,
            request.AccountNumber,
            request.PinCode);
        return result switch
        {
            CreateAccountResult.Success => Ok(),
            CreateAccountResult.SessionNotFound => Unauthorized(),
            CreateAccountResult.Unauthorized => Unauthorized(),
            CreateAccountResult.AccountAlreadyExists => BadRequest(),
            _ => BadRequest(),
        };
    }

    [HttpPost("balance")]
    public ActionResult<AccountBalanceResponse> GetBalance([FromBody] SessionKeyRequest request)
    {
        GetBalanceResult result = _accountService.GetBalance(request.SessionKey);
        return result switch
        {
            GetBalanceResult.Success success => Ok(new AccountBalanceResponse(success.Balance)),
            GetBalanceResult.SessionNotFound => Unauthorized(),
            GetBalanceResult.Unauthorized => Unauthorized(),
            _ => BadRequest(),
        };
    }

    [HttpPost("withdraw")]
    public ActionResult<AccountBalanceResponse> WithdrawMoney([FromBody] ChangeBalanceRequest request)
    {
        WithdrawMoneyResult result = _accountService.WithdrawMoney(request.SessionKey, request.Amount);
        return result switch
        {
            WithdrawMoneyResult.Success success => Ok(new AccountBalanceResponse(success.Balance)),
            WithdrawMoneyResult.SessionNotFound => Unauthorized(),
            WithdrawMoneyResult.Unauthorized => Unauthorized(),
            WithdrawMoneyResult.InvalidAmount => BadRequest(),
            WithdrawMoneyResult.NotEnoughMoney => BadRequest(),
            _ => BadRequest(),
        };
    }

    [HttpPost("deposit")]
    public ActionResult<AccountBalanceResponse> DepositMoney([FromBody] ChangeBalanceRequest request)
    {
        DepositMoneyResult result = _accountService.DepositMoney(request.SessionKey, request.Amount);
        return result switch
        {
            DepositMoneyResult.Success success => Ok(new AccountBalanceResponse(success.Balance)),
            DepositMoneyResult.SessionNotFound => Unauthorized(),
            DepositMoneyResult.Unauthorized => Unauthorized(),
            DepositMoneyResult.InvalidAmount => BadRequest(),
            _ => BadRequest(),
        };
    }

    [HttpPost("history")]
    public ActionResult<AccountOperationHistoryResponse> GetHistory([FromBody] SessionKeyRequest request)
    {
        GetOperationHistoryResult result = _accountService.GetOperationHistory(request.SessionKey);
        return result switch
        {
            GetOperationHistoryResult.Success success => Ok(new AccountOperationHistoryResponse(
                success.Operations
                    .Select(operation => new AccountOperationResponse(
                        operation.AccountNumber,
                        operation.OperationType,
                        operation.Amount,
                        operation.CreatedAtUtc))
                    .ToArray())),
            GetOperationHistoryResult.SessionNotFound => Unauthorized(),
            GetOperationHistoryResult.Unauthorized => Unauthorized(),
            _ => BadRequest(),
        };
    }
}
