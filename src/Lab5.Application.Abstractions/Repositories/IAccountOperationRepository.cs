using Lab5.Application.Models.Operations;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAccountOperationRepository
{
    void AddOperation(AccountOperation operation);

    IReadOnlyCollection<AccountOperation> FindOperationsByAccountNumber(long accountNumber);
}
