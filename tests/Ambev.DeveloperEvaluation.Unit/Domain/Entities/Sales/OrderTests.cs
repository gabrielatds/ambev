using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales;

/// <summary>
/// Contains unit tests for the Order entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class OrderTests
{
    /// <summary>
    /// Tests that when a Not canceled order is canceled, their IsCanceled property changes to true.
    /// </summary>
    [Fact(DisplayName = "Order IsCancelled should change to true when order is cancelled")]
    public void Given_NotCanceledOrder_WhenCancelled_Then_IsCancelledShouldBeTrue()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.IsCancelled = false;

        // Act
        order.Cancel();

        // Assert
        Assert.True(order.IsCancelled);
    }

    /// <summary>
    /// Tests that when an order date is set to the current date.
    /// </summary>
    [Fact(DisplayName = "Order date should change to DateTime.Now")]
    public void Given_Order_When_SetCurrentDate_Then_DateShouldBeDateTimeNow()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();

        // Act
        order.SetCurrentDate();

        // Assert
        Assert.Equal(DateTime.Now.Date, order.Date.Date);
    }

    /// <summary>
    /// Tests that validation passes when all order properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid order data")]
    public void Given_ValidOrderData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();

        // Act
        var result = order.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when order properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid order data")]
    public void Given_InvalidOrderData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var order = new Order
        {
            Number = 0, // Invalid: zero
            Date = DateTime.Now.AddDays(1), // Invalid: future date
            CustomerId = Guid.Empty, // Invalid: empty guid
            CustomerName = OrderTestData.GenerateLongName(), // Invalid: length greater than 100.
            BranchId = Guid.Empty, // Invalid: empty guid
            BranchName = OrderTestData.GenerateLongName(), // Invalid: length greater than 100.
        };

        // Act
        var result = order.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
