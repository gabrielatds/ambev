using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    /// <summary>
    /// Gets the product id.
    /// Must be a valid GUID.
    /// </summary>
    public Guid ProductId { get; private set; }
    
    /// <summary>
    /// Gets the product title.
    /// Must not exceed 100 characters.
    /// </summary>
    public string ProductTitle { get; private set; }
    
    /// <summary>
    /// Gets the product unit price.
    /// Must not be null.
    /// </summary>
    public virtual Money UnitPrice { get; private set; }
    
    /// <summary>
    /// Gets the order item discount value.
    /// </summary>
    public virtual Money Discount { get; private set; }
    
    /// <summary>
    /// Gets the product quantity.
    /// Must be greater than 0.
    /// </summary>
    public int Quantity { get; private set; }
    
    public OrderItem(Guid productId, string productTitle, Money unitPrice, int quantity)
    {
        ProductId = productId;
        ProductTitle = productTitle;
        UnitPrice = unitPrice;
        Quantity = quantity;

        Validate();
    }

    public OrderItem()
    {
    }

    public void IncreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ValidationException("Amount must be greater than zero.");

        Quantity += amount;
    }

    /// <summary>
    /// Removes an 
    /// </summary>
    /// <param name="amount"></param>
    /// <exception cref="ValidationException"></exception>
    public void DecreaseQuantity(int amount)
    {
        if (amount <= 0)
            throw new ValidationException("Amount must be greater than zero.");

        if (amount > Quantity)
            throw new ValidationException("Cannot decrease more than the existing quantity.");

        Quantity -= amount;
    }

    /// <summary>
    /// Gets the total amount substracting discount.
    /// </summary>
    /// <returns></returns>
    public Money TotalAmount()
    {
        return UnitPrice.Multiply(Quantity).Subtract(Discount);
    }

    /// <summary>
    /// Method to set order item discount.
    /// </summary>
    /// <param name="discount"></param>
    public void ApplyDiscount(Money discount)
    {
        Discount = discount;
    }
    
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