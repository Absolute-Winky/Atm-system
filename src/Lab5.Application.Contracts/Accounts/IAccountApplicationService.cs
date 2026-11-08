namespace Lab5.Application.Contracts.Accounts;

public interface IAccountApplicationService
{
    CreateAccountResult CreateAccount(Guid sessionKey, long accountNumber, int pinCode);

    GetBalanceResult GetBalance(Guid sessionKey);

    WithdrawMoneyResult WithdrawMoney(Guid sessionKey, decimal amount);

    DepositMoneyResult DepositMoney(Guid sessionKey, decimal amount);

    GetOperationHistoryResult GetOperationHistory(Guid sessionKey);
}
