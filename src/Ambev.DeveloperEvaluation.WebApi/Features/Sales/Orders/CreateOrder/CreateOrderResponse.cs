namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.CreateOrder;

public class CreateOrderResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created order.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created order in the system.</value>
    public Guid Id { get; set; }
}