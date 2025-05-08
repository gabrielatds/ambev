using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer in the system.
/// </summary>
public class Customer : BaseEntity
{
    /// <summary>
    /// Gets the customer's name.
    /// Must not be empty and must not exceed 100 characters.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the customer's address.
    /// Must be a valid Address value object.
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    /// Gets the customer's email address.
    /// Must be a valid email format.
    /// </summary>
    public string Email { get; private set; }

    // Constructor to ensure a valid customer is created
    public Customer(string name, Address address, string email)
    {
        Name = name;
        Address = address;
        Email = email;

        Validate();
    }

    /// <summary>
    /// Updates the customer's name.
    /// </summary>
    public void UpdateName(string name)
    {
        Name = name;
        Validate();
    }

    /// <summary>
    /// Updates the customer's address.
    /// </summary>
    public void UpdateAddress(Address address)
    {
        Address = address;
        Validate();
    }

    /// <summary>
    /// Updates the customer's email.
    /// </summary>
    public void UpdateEmail(string email)
    {
        Email = email;
        Validate();
    }

    /// <summary>
    /// Performs validation of the customer entity using the CustomerValidator rules.
    /// </summary>
    private ValidationResultDetail Validate()
    {
        var validator = new CustomerValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(f => (ValidationErrorDetail)f)
        };
    }
}
