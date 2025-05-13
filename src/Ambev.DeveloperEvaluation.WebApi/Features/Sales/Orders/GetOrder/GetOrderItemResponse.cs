using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.GetOrder;

public class GetOrderItemResponse
{
    /// <summary>
    /// Gets the order item's unique id.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets the product id.
    /// Must be a valid GUID.
    /// </summary>
    public Guid ProductId { get; private set; }
    
    /// <summary>
    /// Gets the order id.
    /// Must be a valid GUID.
    /// </summary>
    public Guid OrderId { get; private set; }
    
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
    /// Gets the product quantity.
    /// Must be greater than 0.
    /// </summary>
    public int Quantity { get; private set; }
    
    /// <summary>
    /// Gets the order item total amount.
    /// </summary>
    public Money TotalAmount { get; set; }
}