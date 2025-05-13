namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.UpdateOrder;

public class UpdateOrderRequest
{
    /// <summary>
    /// Gets the order's unique id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets the order's number.
    /// Must not be greater than 0.
    /// </summary>
    public long Number { get; set; }
    
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
    public List<UpdateOrderItemRequest> Items { get; set; }
}