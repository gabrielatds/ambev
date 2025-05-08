using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="Branch"/> entity.
/// </summary>
public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty().WithMessage("Branch name must not be empty.")
            .MaximumLength(100).WithMessage("Branch name must not exceed 100 characters.");

        RuleFor(branch => branch.Address)
            .NotNull().WithMessage("Branch address must be provided.");
    }
}