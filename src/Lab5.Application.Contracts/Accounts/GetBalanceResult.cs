namespace Lab5.Application.Contracts.Accounts;

public abstract record GetBalanceResult
{
    private GetBalanceResult()
    {
    }

    public sealed record Success(decimal Balance) : GetBalanceResult;

    public sealed record SessionNotFound : GetBalanceResult;

    public sealed record Unauthorized : GetBalanceResult;
}
