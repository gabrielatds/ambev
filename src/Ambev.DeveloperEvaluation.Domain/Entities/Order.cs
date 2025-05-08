using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a Order in the system with Customer and Branch information
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Order : BaseEntity
{
    /// <summary>
    /// Gets the order's number.
    /// Must not be greater than 0.
    /// </summary>
    public long OrderNumber { get; private set; }
    
    /// <summary>
    /// Gets the order's creation date.
    /// Must not be a future date.
    /// </summary>
    public DateTime OrderDate { get; private set; }
    
    /// <summary>
    /// Gets the order's customer id;
    /// Must be a valid GUID.
    /// </summary>
    public Guid CustomerId { get; private set; }
    
    /// <summary>
    /// Gets the order's customer name;
    /// Must not be an empty and exceed 100 characters.
    /// </summary>
    public string CustomerName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Gets the order's branch id;
    /// Must be a valid GUID.
    /// </summary>
    public Guid BranchId { get; private set; }
    
    /// <summary>
    /// Gets the order's customer name;
    /// Must not be an empty and exceed 100 characters.
    /// </summary>
    public string BranchName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the order's current status.
    /// Indicates whether the order is Cancelled/Not Cancelled.
    /// </summary>
    public bool Cancelled { get; private set; } = false;

    /// <summary>
    /// Gets the order's items.
    /// Must have at least one order item.
    /// </summary>
    public List<OrderItem> Items { get; private set; } = new List<OrderItem>();

    // Constructor to ensure the object is valid when created
    public Order(long orderNumber, DateTime orderDate, Guid customerId, string customerName, Guid branchId, string branchName)
    {
        OrderNumber = orderNumber;
        OrderDate = orderDate;
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;

        Validate();
    }

    /// <summary>
    /// Cancel the order.
    /// </summary>
    public void Cancel()
    {
        this.Cancelled = true;
    }

    /// <summary>
    /// Calculate the total amount for the order
    /// </summary>
    /// <returns></returns>
    public Money TotalAmount()
    {
        var total = Items.Aggregate(Money.Zero, (sum, item) => sum.Add(item.UnitPrice));
        return total;
    }
    
    /// <summary>
    /// Add an item to order's items list
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="productName"></param>
    /// <param name="unitPrice"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentException"></exception>
    public void AddItem(Guid productId, string productName, Money unitPrice, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            var item = new OrderItem(productId, productName, unitPrice, quantity);
            Items.Add(item);
        }
    }

    /// <summary>
    /// Remove an item to order's items list
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="quantity"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void RemoveItem(Guid productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity to remove must be greater than zero.", nameof(quantity));

        var existingItem = Items.FirstOrDefault(i => i.ProductId == productId);

        if (existingItem is null)
            throw new InvalidOperationException("Item not found in the order.");

        existingItem.DecreaseQuantity(quantity);

        if (existingItem.Quantity == 0)
        {
            Items.Remove(existingItem);
        }
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
    private ValidationResultDetail Validate()
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