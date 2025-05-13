namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.CreateOrder;
using FluentValidation;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(order => order.Number)
            .GreaterThan(0).WithMessage("Order number must be greater than 0.");
        
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
        
        RuleForEach(order => order.Items).SetValidator(new CreateOrderItemRequestValidator());
    }
}