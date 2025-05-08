using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string ProductTitle { get; private set; }
    
    public Money UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    
    public OrderItem(Guid productId, string productTitle, Money unitPrice, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        ProductId = productId;
        ProductTitle = productTitle;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.", nameof(amount));

        if (amount > Quantity)
            throw new InvalidOperationException("Cannot decrease more than the existing quantity.");

        Quantity -= amount;
    }

    public Money TotalAmount() => UnitPrice.Multiply(Quantity);
    
    /// <summary>
    /// Performs validation of the order item entity using the OrderItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Order number value</list>
    /// <list type="bullet">Customer name length</list>
    /// <list type="bullet">Branch name length</list>
    /// <list type="bullet">Total of items</list>
    /// </remarks>
    private ValidationResultDetail Validate()
    {
        var validator = new OrderItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(validationFailure
                => (ValidationErrorDetail)validationFailure)
        };
    }
}