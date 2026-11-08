namespace Lab5.Application.Contracts.Sessions;

public abstract record CreateUserSessionResult
{
    private CreateUserSessionResult()
    {
    }

    public sealed record Success(Guid SessionKey) : CreateUserSessionResult;

    public sealed record AccountNotFound : CreateUserSessionResult;

    public sealed record WrongPinCode : CreateUserSessionResult;
}
