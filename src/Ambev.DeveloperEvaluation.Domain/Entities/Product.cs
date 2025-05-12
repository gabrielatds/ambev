using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product available in the catalog.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets the product title.
    /// Must not be empty or exceed 100 characters.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Gets the product price.
    /// Must be a valid monetary value.
    /// </summary>
    public virtual Money Price { get; private set; }

    /// <summary>
    /// Gets the product description.
    /// Optional, max 500 characters.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the product category.
    /// Optional, max 50 characters.
    /// </summary>
    public string Category { get; private set; }

    /// <summary>
    /// Gets the product image URL or path.
    /// Optional, max 255 characters.
    /// </summary>
    public string Image { get; private set; }

    /// <summary>
    /// Gets the rating info (rate and count).
    /// Must be a valid value object.
    /// </summary>
    public virtual Rating Rating { get; private set; }

    /// <summary>
    /// Creates a new product instance.
    /// </summary>
    /// <param name="title">The product title</param>
    /// <param name="price">The product price</param>
    /// <param name="description">The description</param>
    /// <param name="category">The category</param>
    /// <param name="image">The image path or URL</param>
    /// <param name="rating">The rating</param>
    public Product(string title, Money price, string description, string category, string image, Rating rating)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        Rating = rating;
    }

    public Product()
    {
    }

    /// <summary>
    /// Updates the rating of the product.
    /// </summary>
    /// <param name="newRating">The new rating</param>
    public void UpdateRating(Rating newRating)
    {
        Rating = newRating ?? throw new ArgumentNullException(nameof(newRating));
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Title required and max 100 chars</list>
    /// <list type="bullet">Price validity</list>
    /// <list type="bullet">Optional fields length limits</list>
    /// </remarks>
    private ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(validationFailure => (ValidationErrorDetail)validationFailure)
        };
    }
}
