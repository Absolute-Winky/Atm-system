namespace Lab5.Application.Contracts.Sessions;

public abstract record CreateAdminSessionResult
{
    private CreateAdminSessionResult()
    {
    }

    public sealed record Success(Guid SessionKey) : CreateAdminSessionResult;

    public sealed record WrongPassword : CreateAdminSessionResult;
}
