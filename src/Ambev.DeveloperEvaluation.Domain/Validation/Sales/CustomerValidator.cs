using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for the <see cref="Customer"/> entity.
/// </summary>
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty().WithMessage("Customer name must not be empty.")
            .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");

        RuleFor(customer => customer.Address)
            .NotNull().WithMessage("Customer address must be provided.");

        RuleFor(customer => customer.Email).SetValidator(new EmailValidator());
    }
}