using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Operations;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class InMemoryAccountOperationRepository : IAccountOperationRepository
{
    private readonly List<AccountOperation> _operations;

    public InMemoryAccountOperationRepository()
    {
        _operations = [];
    }

    public void AddOperation(AccountOperation operation)
    {
        _operations.Add(operation);
    }

    public IReadOnlyCollection<AccountOperation> FindOperationsByAccountNumber(long accountNumber)
    {
        return _operations
            .Where(operation => operation.AccountNumber == accountNumber)
            .OrderBy(operation => operation.CreatedAtUtc)
            .ToArray();
    }
}
