using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects;

/// <summary>
/// Contains unit tests for the Money value object class.
/// Tests cover arithmetic operations and validation scenarios.
/// </summary>
public class MoneyTests
{
    [Fact(DisplayName = "Money should be created with rounded amount and uppercase currency")]
    public void Given_ValidValues_When_Constructed_Then_ShouldRoundAmountAndUppercaseCurrency()
    {
        // Arrange
        var money = new Money(10.456m, "brl");

        // Assert
        Assert.Equal(10.46m, money.Amount);
        Assert.Equal("BRL", money.Currency);
    }

    [Fact(DisplayName = "Money Add should return a new Money with correct value")]
    public void Given_TwoMoneyWithSameCurrency_When_Added_Then_ShouldReturnSum()
    {
        // Arrange
        var money50 = new Money(50m, "BRL");
        var money20 = new Money(30m, "BRL");

        // Act
        var result = money50.Add(money20);

        // Assert
        Assert.Equal(new Money(80m, "BRL"), result);
    }

    [Fact(DisplayName = "Money Subtract should return a new Money with correct value")]
    public void Given_TwoMoneyWithSameCurrency_When_Subtracted_Then_ShouldReturnDifference()
    {
        // Arrange
        var money50 = new Money(50m, "BRL");
        var money20 = new Money(20m, "BRL");

        // Act
        var result = money50.Subtract(money20);

        // Assert
        Assert.Equal(new Money(30m, "BRL"), result);
    }

    [Fact(DisplayName = "Money Subtract should throw when result is negative")]
    public void Given_SubtractionWithResultingNegativeAmount_When_Subtracted_Then_ShouldThrow()
    {
        // Arrange
        var money10 = new Money(10m, "BRL");
        var money20 = new Money(20m, "BRL");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => money10.Subtract(money20));
    }

    [Fact(DisplayName = "Money Multiply should return correct value")]
    public void Given_ValidMoney_When_Multiplied_Then_ShouldReturnCorrectProduct()
    {
        // Arrange
        var money = new Money(12.5m, "BRL");

        // Act
        var result = money.Multiply(3);

        // Assert
        Assert.Equal(new Money(37.5m, "BRL"), result);
    }

    [Fact(DisplayName = "Money Multiply should throw for negative multiplier")]
    public void Given_NegativeMultiplier_When_Multiplied_Then_ShouldThrow()
    {
        // Arrange
        var money = new Money(10m, "BRL");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => money.Multiply(-1));
    }

    [Fact(DisplayName = "Add should throw if currencies differ")]
    public void Given_MoneyWithDifferentCurrencies_When_Added_Then_ShouldThrow()
    {
        // Arrange
        var money10 = new Money(10m, "BRL");
        var money5 = new Money(5m, "USD");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => money10.Add(money5));
    }

    [Fact(DisplayName = "Subtract should throw if currencies differ")]
    public void Given_MoneyWithDifferentCurrencies_When_Subtracted_Then_ShouldThrow()
    {
        // Arrange
        var money10 = new Money(10m, "BRL");
        var money5 = new Money(5m, "USD");

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => money10.Subtract(money5));
    }

    [Fact(DisplayName = "Equals should return true for same value and currency")]
    public void Given_TwoMoneyObjects_When_Equal_Then_ShouldReturnTrue()
    {
        // Arrange
        var money1 = new Money(10m, "BRL");
        var money2 = new Money(10m, "BRL");

        // Act & Assert
        Assert.True(money1.Equals(money2));
        Assert.Equal(money1.GetHashCode(), money2.GetHashCode());
    }

    [Fact(DisplayName = "Equals should return false for different amount or currency")]
    public void Given_TwoMoneyObjects_When_Different_Then_ShouldReturnFalse()
    {
        // Arrange
        var money1 = new Money(10m, "BRL");
        var money2 = new Money(15m, "BRL");
        var money3 = new Money(10m, "USD");

        // Act & Assert
        Assert.False(money1.Equals(money2));
        Assert.False(money1.Equals(money3));
    }
}
