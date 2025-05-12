using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.Number)
            .GreaterThan(0).WithMessage("Order number must be greater than 0.");
        
        RuleFor(order => order.Date)
            .NotEmpty().WithMessage("Order date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Order date cannot be in the future.");

        RuleFor(order => order.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(order => order.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        RuleFor(order => order.BranchId)
            .NotEmpty().WithMessage("Branch ID is required.");

        RuleFor(order => order.BranchName)
            .NotEmpty().WithMessage("Branch name is required.")
            .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters.");

        RuleFor(order => order.Items)
            .NotEmpty().WithMessage("At least one order item is required.")
            .Must(items => items.All(item => item.Quantity > 0)).WithMessage("Each order item must have a quantity greater than 0.");

        RuleFor(order => order.IsCancelled)
            .Equal(false).WithMessage("Order cannot be cancelled at creation.");
    }
}