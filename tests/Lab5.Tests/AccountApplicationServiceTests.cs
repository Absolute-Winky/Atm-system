using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Sessions;
using Lab5.Application.Services;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class AccountApplicationServiceTests
{
    [Fact]
    public void WithdrawMoney_WithEnoughBalance_UpdatesSavedAccount()
    {
        // Arrange
        var sessionKey = Guid.NewGuid();
        ISessionRepository sessionRepository = Substitute.For<ISessionRepository>();
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IAccountOperationRepository operationRepository = Substitute.For<IAccountOperationRepository>();

        sessionRepository.FindSessionByKey(sessionKey)
            .Returns(new Session(sessionKey, SessionRole.User, 1001, DateTime.UtcNow));
        accountRepository.FindAccountByNumber(1001)
            .Returns(new Account(1001, 1234, 1000m));

        var service = new AccountApplicationService(
            accountRepository,
            operationRepository,
            sessionRepository);

        // Act
        WithdrawMoneyResult result = service.WithdrawMoney(sessionKey, 250m);

        // Assert
        WithdrawMoneyResult.Success success = Assert.IsType<WithdrawMoneyResult.Success>(result);
        Assert.Equal(750m, success.Balance);
        accountRepository.Received(1).UpdateAccount(Arg.Is<Account>(account => account.Balance == 750m));
    }

    [Fact]
    public void WithdrawMoney_WithNotEnoughBalance_ReturnsError()
    {
        // Arrange
        var sessionKey = Guid.NewGuid();
        ISessionRepository sessionRepository = Substitute.For<ISessionRepository>();
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IAccountOperationRepository operationRepository = Substitute.For<IAccountOperationRepository>();

        sessionRepository.FindSessionByKey(sessionKey)
            .Returns(new Session(sessionKey, SessionRole.User, 1001, DateTime.UtcNow));
        accountRepository.FindAccountByNumber(1001)
            .Returns(new Account(1001, 1234, 100m));

        var service = new AccountApplicationService(
            accountRepository,
            operationRepository,
            sessionRepository);

        // Act
        WithdrawMoneyResult result = service.WithdrawMoney(sessionKey, 250m);

        // Assert
        Assert.IsType<WithdrawMoneyResult.NotEnoughMoney>(result);
        accountRepository.DidNotReceive().UpdateAccount(Arg.Any<Account>());
    }

    [Fact]
    public void DepositMoney_ValidAmount_UpdatesSavedAccount()
    {
        // Arrange
        var sessionKey = Guid.NewGuid();
        ISessionRepository sessionRepository = Substitute.For<ISessionRepository>();
        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        IAccountOperationRepository operationRepository = Substitute.For<IAccountOperationRepository>();

        sessionRepository.FindSessionByKey(sessionKey)
            .Returns(new Session(sessionKey, SessionRole.User, 1001, DateTime.UtcNow));
        accountRepository.FindAccountByNumber(1001)
            .Returns(new Account(1001, 1234, 200m));

        var service = new AccountApplicationService(
            accountRepository,
            operationRepository,
            sessionRepository);

        // Act
        DepositMoneyResult result = service.DepositMoney(sessionKey, 50m);

        // Assert
        DepositMoneyResult.Success success = Assert.IsType<DepositMoneyResult.Success>(result);
        Assert.Equal(250m, success.Balance);
        accountRepository.Received(1).UpdateAccount(Arg.Is<Account>(account => account.Balance == 250m));
    }
}
