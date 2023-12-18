using NUnit.Framework;
using System;

namespace TestApp.Tests;

[TestFixture]
public class BankAccountTests
{
    private BankAccount _bankAccount;

    [SetUp]
    public void SetUp()
    {
        double initialBalance = 1000;
        this._bankAccount = new BankAccount(initialBalance);
    }
    [Test]
    public void Test_Constructor_InitialBalanceIsSet()
    {
        // Arrange
        double expectedInitialBalance = 1000.0;

        // Act
        double actualInitialBalance = _bankAccount.Balance;

        // Assert
        Assert.That(actualInitialBalance, Is.EqualTo(expectedInitialBalance));
    }

    [Test]
    public void Test_Deposit_PositiveAmount_IncreasesBalance()
    {
        // Arrange
        double depositAmount = 500.0;
        double expectedBalance = _bankAccount.Balance + depositAmount;

        // Act
        _bankAccount.Deposit(depositAmount);
        double actualBalance = _bankAccount.Balance;

        // Assert
        Assert.That(actualBalance, Is.EqualTo(expectedBalance));
    }

    [Test]
    public void Test_Deposit_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        double invalidDepositAmount = -200.49; // decimals

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _bankAccount.Deposit(invalidDepositAmount));
    }

    [Test]
    public void Test_Withdraw_ValidAmount_DecreasesBalance()
    {
            // Arrange
            double withdrawalAmount = 500.49; // decimals
            double expectedBalance = Math.Round(this._bankAccount.Balance - withdrawalAmount, 2);

            // Act
            _bankAccount.Withdraw(withdrawalAmount);
            double actualBalance = Math.Round(this._bankAccount.Balance, 2);

            // Assert
            Assert.That(actualBalance, Is.EqualTo(expectedBalance));
        
    }

    [Test]
    public void Test_Withdraw_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        double invalidWithdrawalAmount = -200.49;

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _bankAccount.Withdraw(invalidWithdrawalAmount));
    }

    [Test]
    public void Test_Withdraw_AmountGreaterThanBalance_ThrowsArgumentException()
    {
        // Arrange
        double withdrawalAmount = 2000.0;

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _bankAccount.Withdraw(withdrawalAmount));
    }

    [Test]
    public void Test_Withdraw_MaximumAmount_Allowed()
    {
        // Arrange
        double initialBalance = 1000.0;
        double maximumWithdrawalAmount = initialBalance;

        // Act
        _bankAccount.Withdraw(maximumWithdrawalAmount);
        double actualBalance = _bankAccount.Balance;

        // Assert
        Assert.That(actualBalance, Is.EqualTo(0.0));
    }

    [Test]
    public void Test_Withdraw_MaximumAmount_PlusOne_ThrowsArgumentException()
    {
        // Arrange
        double initialBalance = 1000.0;
        double maximumWithdrawalAmount = initialBalance + 1.0;

        // Act and Assert
        Assert.Throws<ArgumentException>(() => _bankAccount.Withdraw(maximumWithdrawalAmount));
    }
    [Test]
    public void Test_Deposit_PositiveAmount_AfterWithdrawal_IncreasesBalance()
    {
        // Arrange
        double initialBalance = 1000.0;
        _bankAccount = new BankAccount(initialBalance);

        // Act
        _bankAccount.Withdraw(500.0);
        _bankAccount.Deposit(200.0);
        double expectedBalance = initialBalance - 500.0 + 200.0;
        double actualBalance = _bankAccount.Balance;

        // Assert
        Assert.That(actualBalance, Is.EqualTo(expectedBalance));
    }
    
}
