using Lab5.Application.Models.Operations;

namespace Lab5.Controllers;

public record AccountOperationResponse(
    long AccountNumber,
    AccountOperationType OperationType,
    decimal Amount,
    DateTime CreatedAtUtc);
