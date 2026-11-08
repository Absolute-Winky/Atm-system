using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Accounts;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class InMemoryAccountRepository : IAccountRepository
{
    private readonly List<Account> _accounts;

    public InMemoryAccountRepository()
    {
        _accounts = [];
    }

    public Account? FindAccountByNumber(long accountNumber)
    {
        return _accounts.FirstOrDefault(account => account.Number == accountNumber);
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public void UpdateAccount(Account account)
    {
        Account? existingAccount = FindAccountByNumber(account.Number);
        if (existingAccount is null)
        {
            return;
        }

        _accounts.Remove(existingAccount);
        _accounts.Add(account);
    }
}
