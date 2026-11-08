using Lab5.Application.Models.Operations;

namespace Lab5.Application.Contracts.Accounts;

public abstract record GetOperationHistoryResult
{
    private GetOperationHistoryResult()
    {
    }

    public sealed record Success(IReadOnlyCollection<AccountOperation> Operations) : GetOperationHistoryResult;

    public sealed record SessionNotFound : GetOperationHistoryResult;

    public sealed record Unauthorized : GetOperationHistoryResult;
}
