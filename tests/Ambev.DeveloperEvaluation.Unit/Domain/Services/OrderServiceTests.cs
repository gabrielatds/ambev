using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Services;

/// <summary>
/// Tests that OrderService selects the correct discount strategy based on business rules:
/// - No discount under 4 items
/// - 10% discount from 4 to 9
/// - 20% discount from 10 to 20
/// - Throws exception for more than 20
/// </summary>
public class OrderServiceTests
{
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _service = new OrderService();
    }

    [Theory(DisplayName = "Should apply NO discount for quantity < 4")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Given_QuantityLessThan4_When_GetDiscount_Then_NoDiscount(int quantity)
    {
        // Arrange
        decimal unitPrice = 100.0m;

        // Act
        var discount = _service.GetDiscount(quantity, unitPrice);

        // Assert
        discount.Should().Be(0);
    }

    [Theory(DisplayName = "Should apply 10% discount for quantity 4 to 9")]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(9)]
    public void Given_QuantityBetween4And9_When_GetDiscount_Then_Apply10PercentDiscount(int quantity)
    {
        // Arrange
        decimal unitPrice = 100.0m;
        var expectedDiscount = unitPrice * quantity * 0.10m;

        // Act
        var discount = _service.GetDiscount(quantity, unitPrice);

        // Assert
        discount.Should().Be(expectedDiscount);
    }

    [Theory(DisplayName = "Should apply 20% discount for quantity 10 to 20")]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(20)]
    public void Given_QuantityBetween10And20_When_GetDiscount_Then_Apply20PercentDiscount(int quantity)
    {
        // Arrange
        decimal unitPrice = 100.0m;
        var expectedDiscount = unitPrice * quantity * 0.20m;

        // Act
        var discount = _service.GetDiscount(quantity, unitPrice);

        // Assert
        discount.Should().BeApproximately(expectedDiscount, 0.001m);
    }

    [Theory(DisplayName = "Should throw ValidationException when quantity exceeds 20")]
    [InlineData(21)]
    [InlineData(25)]
    [InlineData(100)]
    public void Given_QuantityGreaterThan20_When_GetDiscount_Then_ThrowsException(int quantity)
    {
        // Arrange
        decimal unitPrice = 100.0m;

        // Act
        var act = () => _service.GetDiscount(quantity, unitPrice);

        // Assert
        act.Should().Throw<ValidationException>();
    }
}
