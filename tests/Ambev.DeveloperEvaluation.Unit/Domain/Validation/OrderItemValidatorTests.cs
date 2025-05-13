using Ambev.DeveloperEvaluation.Domain.Validation.Sales;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

/// <summary>
/// Contains unit tests for the OrderItemValidator class.
/// Tests cover validation of order item properties including product ID,
/// product title, unit price, and quantity rules.
/// </summary>
public class OrderItemValidatorTests
{
    private readonly OrderItemValidator _validator;

    public OrderItemValidatorTests()
    {
        _validator = new OrderItemValidator();
    }

    /// <summary>
    /// Tests that validation passes when all order item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Valid order item should pass all validation rules")]
    public void Given_ValidOrderItem_When_Validated_Then_ShouldNotHaveErrors()
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Tests that validation fails when product ID is empty.
    /// </summary>
    [Fact(DisplayName = "Empty product ID should fail validation")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();
        item.ProductId = Guid.Empty;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId);
    }

    /// <summary>
    /// Tests that validation fails when product title is null, empty, or too long.
    /// </summary>
    [Theory(DisplayName = "Invalid product title should fail validation")]
    [InlineData("")]
    [InlineData(null)]
    public void Given_InvalidProductTitle_When_Validated_Then_ShouldHaveError(string title)
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();
        item.ProductTitle = title;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductTitle);
    }

    [Fact(DisplayName = "Product title exceeding max length should fail validation")]
    public void Given_ProductTitleTooLong_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();
        item.ProductTitle = OrderItemTestData.GenerateLongName();

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductTitle);
    }

    /// <summary>
    /// Tests that validation fails when unit price is null.
    /// </summary>
    [Fact(DisplayName = "Null unit price should fail validation")]
    public void Given_NullUnitPrice_When_Validated_Then_ShouldHaveError()
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();
        item.UnitPrice = null;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }

    /// <summary>
    /// Tests that validation fails when quantity is zero or negative.
    /// </summary>
    [Theory(DisplayName = "Invalid quantity should fail validation")]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_InvalidQuantity_When_Validated_Then_ShouldHaveError(int quantity)
    {
        // Arrange
        var item = OrderItemTestData.GenerateValidOrderItem();
        item.Quantity = quantity;

        // Act
        var result = _validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
}
