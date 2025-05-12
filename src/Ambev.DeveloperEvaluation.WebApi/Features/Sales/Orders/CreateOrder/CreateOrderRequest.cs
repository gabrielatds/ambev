namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.CreateOrder;

public class CreateOrderRequest
{
    /// <summary>
    /// Gets the order's number.
    /// Must not be greater than 0.
    /// </summary>
    public long OrderNumber { get; set; }
    
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
    /// Gets the order's items.
    /// Must have at least one order item.
    /// </summary>
    public List<OrderItemRequest> Items { get; set; }
}