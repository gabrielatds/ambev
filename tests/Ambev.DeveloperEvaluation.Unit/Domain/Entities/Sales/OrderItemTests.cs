using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales;

/// <summary>
/// Contains unit tests for the OrderItem entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class OrderItemTests
{
    /// <summary>
    /// Tests that when an order item total amount is set.
    /// </summary>
    [Fact(DisplayName = "Order Item total amount should be calculated")]
    public void Given_OrderItem_When_SetTotalAmount_Then_ShouldBeEqualThanQuantityAndUnitPrice()
    {
        // Arrange
        var orderItem = OrderItemTestData.GenerateValidOrderItem();

        // Act
        orderItem.SetTotalAmount();

        // Assert
        Assert.Equal(orderItem.TotalAmount, orderItem.UnitPrice.Multiply(orderItem.Quantity).Subtract(orderItem.Discount));
    }
    
    /// <summary>
    /// Tests that when an order item has discount applied.
    /// </summary>
    [Fact(DisplayName = "Order Item discount should change")]
    public void Given_OrderItem_When_ApplyDiscount_Then_ShouldBeAppliedDiscountProperty()
    {
        // Arrange
        var orderItem = OrderItemTestData.GenerateValidOrderItem();
        var discount = new Money(20, "BRL");

        // Act
        orderItem.ApplyDiscount(discount);

        // Assert
        Assert.Equal(orderItem.Discount, discount);
    }

    /// <summary>
    /// Tests that validation passes when all order item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid order item data")]
    public void Given_ValidOrderItemData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var orderItem = OrderItemTestData.GenerateValidOrderItem();

        // Act
        var result = orderItem.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when order properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid order item data")]
    public void Given_InvalidOrderItemData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var orderItem = new OrderItem
        {
            ProductId = Guid.Empty, // Invalid: empty guid.
            ProductTitle = OrderTestData.GenerateLongName(), // Invalid: length greater than 100.
            OrderId = Guid.Empty, // Invalid: empty guid.
            UnitPrice = null, // Invalid: null.
            TotalAmount = null, // Invalid: null.
            Discount = null, // Invalid: null.
        };

        // Act
        var result = orderItem.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
