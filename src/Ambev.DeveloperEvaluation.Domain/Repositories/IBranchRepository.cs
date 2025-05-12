using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Branch entity operations
/// </summary>
public interface IBranchRepository
{
    /// <summary>
    /// Creates a new Branch in the repository
    /// </summary>
    /// <param name="branch">The Branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Branch</returns>
    Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a Branch by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Branch if found, null otherwise</returns>
    Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a Branch from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the Branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Branch was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates a Branch from the repositories
    /// </summary>
    /// <param name="branch">The updated Branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default);
}