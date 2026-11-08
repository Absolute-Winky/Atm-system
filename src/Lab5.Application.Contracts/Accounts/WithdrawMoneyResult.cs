namespace Lab5.Application.Contracts.Accounts;

public abstract record WithdrawMoneyResult
{
    private WithdrawMoneyResult()
    {
    }

    public sealed record Success(decimal Balance) : WithdrawMoneyResult;

    public sealed record SessionNotFound : WithdrawMoneyResult;

    public sealed record Unauthorized : WithdrawMoneyResult;

    public sealed record InvalidAmount : WithdrawMoneyResult;

    public sealed record NotEnoughMoney : WithdrawMoneyResult;
}
