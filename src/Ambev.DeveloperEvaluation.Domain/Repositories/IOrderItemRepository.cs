using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for order item entity operations
/// </summary>
public interface IOrderItemRepository
{
    /// <summary>
    /// Creates a new order item in the repository
    /// </summary>
    /// <param name="orderItem">The order item to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order item</returns>
    Task<OrderItem> CreateAsync(OrderItem orderItem, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an order item by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the order item</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order item if found, null otherwise</returns>
    Task<OrderItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an order item from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the order item to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the order item was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an order item from the repository
    /// </summary>
    /// <param name="orderItem">The order item to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated OrderItem</returns>
    Task<OrderItem> UpdateAsync(OrderItem orderItem, CancellationToken cancellationToken = default);
}