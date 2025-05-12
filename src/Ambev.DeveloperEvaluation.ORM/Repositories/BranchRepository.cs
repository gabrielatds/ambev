using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBranchRepository using Entity Framework Core
/// </summary>
public class BranchRepository : IBranchRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BranchRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BranchRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Branch in the database
    /// </summary>
    /// <param name="branch">The Branch to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Branch</returns>
    public async Task<Branch> CreateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Retrieves a Branch by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Branch</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Branch if found, null otherwise</returns>
    public async Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Branches.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates a Branch from the repository
    /// </summary>
    /// <param name="branch">The Branch to update/param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated Branch</returns>
    public async Task<Branch> UpdateAsync(Branch branch, CancellationToken cancellationToken = default)
    {
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return branch;
    }

    /// <summary>
    /// Deletes a Branch from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Branch to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Branch was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var branch = await GetByIdAsync(id, cancellationToken);
        if (branch == null)
            return false;

        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
