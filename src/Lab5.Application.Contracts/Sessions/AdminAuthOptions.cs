namespace Lab5.Application.Contracts.Sessions;

public class AdminAuthOptions
{
    public static string SectionName => "AdminAuth";

    public string SystemPassword { get; set; } = string.Empty;
}
