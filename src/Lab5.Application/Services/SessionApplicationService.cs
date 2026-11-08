using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Sessions;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Sessions;
using Microsoft.Extensions.Options;

namespace Lab5.Application.Services;

public class SessionApplicationService : ISessionApplicationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ISessionRepository _sessionRepository;
    private readonly AdminAuthOptions _adminAuthOptions;

    public SessionApplicationService(
        IAccountRepository accountRepository,
        ISessionRepository sessionRepository,
        IOptions<AdminAuthOptions> adminAuthOptions)
    {
        _accountRepository = accountRepository;
        _sessionRepository = sessionRepository;
        _adminAuthOptions = adminAuthOptions.Value;
    }

    public CreateUserSessionResult CreateUserSession(long accountNumber, int pinCode)
    {
        Account? account = _accountRepository.FindAccountByNumber(accountNumber);
        if (account is null)
        {
            return new CreateUserSessionResult.AccountNotFound();
        }

        if (account.PinCode != pinCode)
        {
            return new CreateUserSessionResult.WrongPinCode();
        }

        var session = new Session(
            key: Guid.NewGuid(),
            role: SessionRole.User,
            accountNumber: account.Number,
            createdAtUtc: DateTime.UtcNow);
        _sessionRepository.AddSession(session);

        return new CreateUserSessionResult.Success(session.Key);
    }

    public CreateAdminSessionResult CreateAdminSession(string password)
    {
        if (password != _adminAuthOptions.SystemPassword)
        {
            return new CreateAdminSessionResult.WrongPassword();
        }

        var session = new Session(
            key: Guid.NewGuid(),
            role: SessionRole.Admin,
            accountNumber: null,
            createdAtUtc: DateTime.UtcNow);
        _sessionRepository.AddSession(session);

        return new CreateAdminSessionResult.Success(session.Key);
    }

    public void CloseSession(Guid sessionKey)
    {
        _sessionRepository.RemoveSession(sessionKey);
    }
}
