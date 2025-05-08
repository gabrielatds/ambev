using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch of the company with a name and address.
/// </summary>
public class Branch : BaseEntity
{
    /// <summary>
    /// Gets the name of the branch.
    /// Must not be empty and must not exceed 100 characters.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the address of the branch.
    /// Must be a valid Address value object.
    /// </summary>
    public Address Address { get; private set; }

    // Constructor ensures the entity is valid upon creation
    public Branch(string name, Address address)
    {
        Name = name;
        Address = address;

        Validate();
    }

    /// <summary>
    /// Updates the name of the branch.
    /// </summary>
    public void UpdateName(string name)
    {
        Name = name;
        Validate();
    }

    /// <summary>
    /// Updates the address of the branch.
    /// </summary>
    public void UpdateAddress(Address address)
    {
        Address = address;
        Validate();
    }

    /// <summary>
    /// Performs validation of the branch entity using the BranchValidator rules.
    /// </summary>
    private ValidationResultDetail Validate()
    {
        var validator = new BranchValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(f => (ValidationErrorDetail)f)
        };
    }
}