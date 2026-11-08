using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Operations;
using Lab5.Application.Models.Sessions;

namespace Lab5.Application.Services;

public class AccountApplicationService : IAccountApplicationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountOperationRepository _accountOperationRepository;
    private readonly ISessionRepository _sessionRepository;

    public AccountApplicationService(
        IAccountRepository accountRepository,
        IAccountOperationRepository accountOperationRepository,
        ISessionRepository sessionRepository)
    {
        _accountRepository = accountRepository;
        _accountOperationRepository = accountOperationRepository;
        _sessionRepository = sessionRepository;
    }

    public CreateAccountResult CreateAccount(Guid sessionKey, long accountNumber, int pinCode)
    {
        Session? session = _sessionRepository.FindSessionByKey(sessionKey);
        if (session is null)
        {
            return new CreateAccountResult.SessionNotFound();
        }

        if (session.Role != SessionRole.Admin)
        {
            return new CreateAccountResult.Unauthorized();
        }

        Account? existingAccount = _accountRepository.FindAccountByNumber(accountNumber);
        if (existingAccount is not null)
        {
            return new CreateAccountResult.AccountAlreadyExists();
        }

        var account = new Account(accountNumber, pinCode, 0m);
        _accountRepository.AddAccount(account);
        AddHistoryRecord(accountNumber, AccountOperationType.AccountCreated, 0m);

        return new CreateAccountResult.Success();
    }

    public GetBalanceResult GetBalance(Guid sessionKey)
    {
        Session? session = _sessionRepository.FindSessionByKey(sessionKey);
        if (session is null)
        {
            return new GetBalanceResult.SessionNotFound();
        }

        if (session.Role != SessionRole.User || session.AccountNumber is null)
        {
            return new GetBalanceResult.Unauthorized();
        }

        Account? account = _accountRepository.FindAccountByNumber(session.AccountNumber.Value);
        if (account is null)
        {
            return new GetBalanceResult.SessionNotFound();
        }

        AddHistoryRecord(account.Number, AccountOperationType.BalanceViewed, 0m);

        return new GetBalanceResult.Success(account.Balance);
    }

    public WithdrawMoneyResult WithdrawMoney(Guid sessionKey, decimal amount)
    {
        if (amount <= 0m)
        {
            return new WithdrawMoneyResult.InvalidAmount();
        }

        Session? session = _sessionRepository.FindSessionByKey(sessionKey);
        if (session is null)
        {
            return new WithdrawMoneyResult.SessionNotFound();
        }

        if (session.Role != SessionRole.User || session.AccountNumber is null)
        {
            return new WithdrawMoneyResult.Unauthorized();
        }

        Account? account = _accountRepository.FindAccountByNumber(session.AccountNumber.Value);
        if (account is null)
        {
            return new WithdrawMoneyResult.SessionNotFound();
        }

        if (account.Balance < amount)
        {
            return new WithdrawMoneyResult.NotEnoughMoney();
        }

        var updatedAccount = new Account(account.Number, account.PinCode, account.Balance - amount);
        _accountRepository.UpdateAccount(updatedAccount);
        AddHistoryRecord(updatedAccount.Number, AccountOperationType.MoneyWithdrawn, amount);

        return new WithdrawMoneyResult.Success(updatedAccount.Balance);
    }

    public DepositMoneyResult DepositMoney(Guid sessionKey, decimal amount)
    {
        if (amount <= 0m)
        {
            return new DepositMoneyResult.InvalidAmount();
        }

        Session? session = _sessionRepository.FindSessionByKey(sessionKey);
        if (session is null)
        {
            return new DepositMoneyResult.SessionNotFound();
        }

        if (session.Role != SessionRole.User || session.AccountNumber is null)
        {
            return new DepositMoneyResult.Unauthorized();
        }

        Account? account = _accountRepository.FindAccountByNumber(session.AccountNumber.Value);
        if (account is null)
        {
            return new DepositMoneyResult.SessionNotFound();
        }

        var updatedAccount = new Account(account.Number, account.PinCode, account.Balance + amount);
        _accountRepository.UpdateAccount(updatedAccount);
        AddHistoryRecord(updatedAccount.Number, AccountOperationType.MoneyDeposited, amount);

        return new DepositMoneyResult.Success(updatedAccount.Balance);
    }

    public GetOperationHistoryResult GetOperationHistory(Guid sessionKey)
    {
        Session? session = _sessionRepository.FindSessionByKey(sessionKey);
        if (session is null)
        {
            return new GetOperationHistoryResult.SessionNotFound();
        }

        if (session.Role != SessionRole.User || session.AccountNumber is null)
        {
            return new GetOperationHistoryResult.Unauthorized();
        }

        IReadOnlyCollection<AccountOperation> operations = _accountOperationRepository
            .FindOperationsByAccountNumber(session.AccountNumber.Value);

        return new GetOperationHistoryResult.Success(operations);
    }

    private void AddHistoryRecord(long accountNumber, AccountOperationType operationType, decimal amount)
    {
        var operation = new AccountOperation(
            accountNumber: accountNumber,
            operationType: operationType,
            amount: amount,
            createdAtUtc: DateTime.UtcNow);
        _accountOperationRepository.AddOperation(operation);
    }
}
