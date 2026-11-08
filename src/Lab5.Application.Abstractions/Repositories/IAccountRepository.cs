using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Account? FindAccountByNumber(long accountNumber);

    void AddAccount(Account account);

    void UpdateAccount(Account account);
}
