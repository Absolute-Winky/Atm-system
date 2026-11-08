namespace Lab5.Application.Models.Accounts;

public class Account
{
    public Account(long number, int pinCode, decimal balance)
    {
        Number = number;
        PinCode = pinCode;
        Balance = balance;
    }

    public long Number { get; }

    public int PinCode { get; }

    public decimal Balance { get; }
}
