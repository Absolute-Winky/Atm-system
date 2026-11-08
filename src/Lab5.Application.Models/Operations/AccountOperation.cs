namespace Lab5.Application.Models.Operations;

public class AccountOperation
{
    public AccountOperation(
        long accountNumber,
        AccountOperationType operationType,
        decimal amount,
        DateTime createdAtUtc)
    {
        AccountNumber = accountNumber;
        OperationType = operationType;
        Amount = amount;
        CreatedAtUtc = createdAtUtc;
    }

    public long AccountNumber { get; }

    public AccountOperationType OperationType { get; }

    public decimal Amount { get; }

    public DateTime CreatedAtUtc { get; }
}
