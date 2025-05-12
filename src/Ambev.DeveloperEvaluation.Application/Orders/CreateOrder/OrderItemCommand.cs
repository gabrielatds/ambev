using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class OrderItemCommand
{
    /// <summary>
    /// Gets the product id.
    /// Must be a valid GUID.
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Gets the product title.
    /// Must not exceed 100 characters.
    /// </summary>
    public string ProductTitle { get; set; }
    
    /// <summary>
    /// Gets the product unit price.
    /// Must not be null.
    /// </summary>
    public virtual Money UnitPrice { get; set; }
    
    /// <summary>
    /// Gets the product quantity.
    /// Must be greater than 0.
    /// </summary>
    public int Quantity { get; set; }
    
    public ValidationResultDetail Validate()
    {
        var validator = new OrderItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}