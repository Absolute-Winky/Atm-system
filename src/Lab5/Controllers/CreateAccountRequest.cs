namespace Lab5.Controllers;

public record CreateAccountRequest(Guid SessionKey, long AccountNumber, int PinCode);
