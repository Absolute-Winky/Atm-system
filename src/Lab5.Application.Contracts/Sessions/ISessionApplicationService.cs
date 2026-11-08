namespace Lab5.Application.Contracts.Sessions;

public interface ISessionApplicationService
{
    CreateUserSessionResult CreateUserSession(long accountNumber, int pinCode);

    CreateAdminSessionResult CreateAdminSession(string password);

    void CloseSession(Guid sessionKey);
}
