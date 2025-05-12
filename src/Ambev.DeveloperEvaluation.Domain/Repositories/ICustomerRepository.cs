using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Customer entity operations
/// </summary>
public interface ICustomerRepository
{
    /// <summary>
    /// Creates a new Customer in the repository
    /// </summary>
    /// <param name="customer">The Customer to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Customer</returns>
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Customer by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Customer</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Customer if found, null otherwise</returns>
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Customer from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Customer to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Customer was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a Customer from the repository
    /// </summary>
    /// <param name="customer">The Customer to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated customer</returns>
    Task<Customer> UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
}