namespace Lab5.Controllers;

public record ChangeBalanceRequest(Guid SessionKey, decimal Amount);
