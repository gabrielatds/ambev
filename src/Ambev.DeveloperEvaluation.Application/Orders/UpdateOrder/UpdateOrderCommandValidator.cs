using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
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
        
        RuleForEach(order => order.Items).SetValidator(new UpdateOrderItemCommandValidator());
    }
}