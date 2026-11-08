namespace Lab5.Application.Models.Sessions;

public class Session
{
    public Session(Guid key, SessionRole role, long? accountNumber, DateTime createdAtUtc)
    {
        Key = key;
        Role = role;
        AccountNumber = accountNumber;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Key { get; }

    public SessionRole Role { get; }

    public long? AccountNumber { get; }

    public DateTime CreatedAtUtc { get; }
}
