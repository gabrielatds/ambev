using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Order entity operations
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Creates a new order in the repository
    /// </summary>
    /// <param name="order">The order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order</returns>
    Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order if found, null otherwise</returns>
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a List of Orders
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order's list if found, empty otherwise</returns>
    Task<List<Order>> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order by their unique order number
    /// </summary>
    /// <param name="orderNumber">The unique number of the order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order if found, null otherwise</returns>
    Task<Order?> GetByOrderNumberAsync(long orderNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an order from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the order was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an order from the repository
    /// </summary>
    /// <param name="order">The Order to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Order</returns>
    Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);
}