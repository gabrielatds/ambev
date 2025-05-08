using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for <see cref="Product"/> entity.
/// Ensures that all business rules are enforced for a product.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty().WithMessage("Product title must not be empty.")
            .MaximumLength(100).WithMessage("Product title must not exceed 100 characters.");

        RuleFor(product => product.Price)
            .NotNull().WithMessage("Product price must be provided.")
            .Must(price => price.Amount >= 0).WithMessage("Price must be zero or greater.");

        RuleFor(product => product.Description)
            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.")
            .When(p => !string.IsNullOrEmpty(p.Description));

        RuleFor(product => product.Category)
            .MaximumLength(50).WithMessage("Product category must not exceed 50 characters.")
            .When(p => !string.IsNullOrEmpty(p.Category));

        RuleFor(product => product.Image)
            .MaximumLength(255).WithMessage("Product image path must not exceed 255 characters.")
            .When(p => !string.IsNullOrEmpty(p.Image));

        RuleFor(product => product.Rating)
            .NotNull().WithMessage("Rating must be provided.");
    }
}