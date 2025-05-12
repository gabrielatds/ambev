using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IOrderRepository using Entity Framework Core
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of OrderRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public OrderRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Order in the database
    /// </summary>
    /// <param name="order">The Order to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Order</returns>
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    /// <summary>
    /// Retrieves an Order by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Order</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Order if found, null otherwise</returns>
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Include(order => order.Items)
                                    .FirstOrDefaultAsync(order=> order.Id == id, cancellationToken);
    }
    
    /// <summary>
    /// Retrieves an Order by their unique number
    /// </summary>
    /// <param name="orderNumber">The unique identifier of the OrderItem</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Order if found, null otherwise</returns>
    public async Task<Order?> GetByOrderNumberAsync(long orderNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.FirstOrDefaultAsync(order=> order.Number == orderNumber, cancellationToken);
    }
    
    /// <summary>
    /// Retrieves a List of Orders
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order's list if found, empty otherwise</returns>
    public async Task<List<Order>> Get(CancellationToken cancellationToken = default)
    {
        return await _context.Orders.Include(order => order.Items)
                                    .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an Order from the repository
    /// </summary>
    /// <param name="order">The Order to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated order</returns>
    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    /// <summary>
    /// Deletes an Order from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Order to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Order was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await GetByIdAsync(id, cancellationToken);
        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
