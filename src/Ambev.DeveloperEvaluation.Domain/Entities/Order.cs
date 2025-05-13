using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an Order in the system with Customer and Branch information
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets the order's number.
    /// Must not be greater than 0.
    /// </summary>
    public long Number { get; set; }
    
    /// <summary>
    /// Gets the order's creation date.
    /// Must not be a future date.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Gets the order's customer id;
    /// Must be a valid GUID.
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Gets the order's customer name;
    /// Must not be an empty and exceed 100 characters.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets the order's branch id;
    /// Must be a valid GUID.
    /// </summary>
    public Guid BranchId { get; set; }
    
    /// <summary>
    /// Gets the order's customer name;
    /// Must not be an empty and exceed 100 characters.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the order's current status.
    /// Indicates whether the order is Cancelled/Not Cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    /// <summary>
    /// Gets the order's items.
    /// Must have at least one order item.
    /// </summary>
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();


    public Order()
    {
    }

    public void SetCurrentDate()
    {
        this.Date = DateTime.Now;
    }

    /// <summary>
    /// Cancel the order.
    /// </summary>
    public void Cancel()
    {
        this.IsCancelled = true;
    }

    /// <summary>
    /// Performs validation of the order entity using the OrderValidator rules.
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
    public ValidationResultDetail Validate()
    {
        var validator = new OrderValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(validationFailure => (ValidationErrorDetail)validationFailure)
        };
    }
}