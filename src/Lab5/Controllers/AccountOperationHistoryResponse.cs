namespace Lab5.Controllers;

public record AccountOperationHistoryResponse(IReadOnlyCollection<AccountOperationResponse> Operations);
