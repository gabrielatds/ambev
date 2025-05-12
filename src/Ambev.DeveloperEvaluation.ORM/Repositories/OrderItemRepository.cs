using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IOrderItemRepository using Entity Framework Core
/// </summary>
public class OrderItemRepository : IOrderItemRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of OrderItemRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public OrderItemRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new OrderItem in the database
    /// </summary>
    /// <param name="orderItem">The OrderItem to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created OrderItem</returns>
    public async Task<OrderItem> CreateAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        await _context.OrderItems.AddAsync(orderItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return orderItem;
    }

    /// <summary>
    /// Retrieves a OrderItem by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the OrderItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The OrderItem if found, null otherwise</returns>
    public async Task<OrderItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.OrderItems.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates a OrderItem from the repository
    /// </summary>
    /// <param name="orderItem">The OrderItem to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated OrderItem</returns>
    public async Task<OrderItem> UpdateAsync(OrderItem orderItem, CancellationToken cancellationToken = default)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync(cancellationToken);
        return orderItem;
    }

    /// <summary>
    /// Deletes a OrderItem from the database
    /// </summary>
    /// <param name="id">The unique identifier of the OrderItem to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the OrderItem was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var orderItem = await GetByIdAsync(id, cancellationToken);
        if (orderItem == null)
            return false;

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
