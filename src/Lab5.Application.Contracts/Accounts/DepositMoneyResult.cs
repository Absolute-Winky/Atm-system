namespace Lab5.Application.Contracts.Accounts;

public abstract record DepositMoneyResult
{
    private DepositMoneyResult()
    {
    }

    public sealed record Success(decimal Balance) : DepositMoneyResult;

    public sealed record SessionNotFound : DepositMoneyResult;

    public sealed record Unauthorized : DepositMoneyResult;

    public sealed record InvalidAmount : DepositMoneyResult;
}
