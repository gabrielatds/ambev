using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Response model for GetOrder operation
/// </summary>
public class GetOrderResult
{
    /// <summary>
    /// Gets the order's number.
    /// Must not be greater than 0.
    /// </summary>
    public long Number { get; private set; }
    
    /// <summary>
    /// Gets the order's creation date.
    /// Must not be a future date.
    /// </summary>
    public DateTime Date { get; private set; }
    
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
    public bool IsCancelled { get; private set; } = false;

    /// <summary>
    /// Gets the order's items.
    /// Must have at least one order item.
    /// </summary>
    public List<OrderItemResult> Items { get; private set; } = new List<OrderItemResult>();
}
