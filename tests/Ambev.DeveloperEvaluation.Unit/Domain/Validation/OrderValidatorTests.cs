using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the OrderValidator class.
/// Tests cover validation of order properties including number, date,
/// customer and branch information, item rules, and cancellation status.
/// </summary>
public class OrderValidatorTests
{
    private readonly OrderValidator _validator;

    public OrderValidatorTests()
    {
        _validator = new OrderValidator();
    }

    /// <summary>
    /// Tests that validation passes when all order properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid order should pass all validation rules")]
    public void Given_ValidOrder_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when order number is less than or equal to 0.
    /// </summary>
    [Theory(DisplayName = "Invalid order numbers should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidOrderNumber_When_Validated_Then_ShouldHaveError(int number)
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.Number = number;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    /// <summary>
    /// Tests that validation fails when order date is in the future.
    /// </summary>
    [Fact(DisplayName = "Future order date should fail validation")]
    public void Given_FutureOrderDate_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.Date = DateTime.Now.AddMinutes(1);

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Date);
    }

    /// <summary>
    /// Tests that validation fails when customer ID is empty.
    /// </summary>
    [Fact(DisplayName = "Empty customer ID should fail validation")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.CustomerId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    /// <summary>
    /// Tests that validation fails when customer name is empty or too long.
    /// </summary>
    [Theory(DisplayName = "Invalid customer name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidCustomerName_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.CustomerName = name;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    [Fact(DisplayName = "Customer name exceeding max length should fail validation")]
    public void Given_CustomerNameTooLong_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.CustomerName = OrderTestData.GenerateLongName();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerName);
    }

    /// <summary>
    /// Tests that validation fails when branch ID is empty.
    /// </summary>
    [Fact(DisplayName = "Empty branch ID should fail validation")]
    public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.BranchId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchId);
    }

    /// <summary>
    /// Tests that validation fails when branch name is empty or too long.
    /// </summary>
    [Theory(DisplayName = "Invalid branch name should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidBranchName_When_Validated_Then_ShouldHaveError(string name)
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.BranchName = name;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    [Fact(DisplayName = "Branch name exceeding max length should fail validation")]
    public void Given_BranchNameTooLong_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.BranchName = OrderTestData.GenerateLongName();

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchName);
    }

    /// <summary>
    /// Tests that validation fails when order items list is empty.
    /// </summary>
    [Fact(DisplayName = "Empty order items should fail validation")]
    public void Given_EmptyOrderItems_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.Items = [];

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that validation fails when any item has a quantity less than or equal to 0.
    /// </summary>
    [Fact(DisplayName = "Order items with invalid quantity should fail validation")]
    public void Given_ItemWithInvalidQuantity_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.Items[0].Quantity = 0;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }

    /// <summary>
    /// Tests that validation fails when order is marked as cancelled on creation.
    /// </summary>
    [Fact(DisplayName = "Cancelled order on creation should fail validation")]
    public void Given_CancelledOrderOnCreation_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var order = OrderTestData.GenerateValidOrder();
        order.IsCancelled = true;

        // Act
        var result = _validator.TestValidate(order);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IsCancelled);
    }
}
