using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.UpdateOrder;

public class UpdateOrderItemRequestValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemRequestValidator()
    {
        RuleFor(orderItem => orderItem.ProductId)
            .NotEmpty()
            .WithMessage("Product ID must be a valid GUID.");

        RuleFor(orderItem => orderItem.ProductTitle)
            .NotEmpty()
            .WithMessage("Product title is required.")
            .MaximumLength(100)
            .WithMessage("Product title cannot exceed 100 characters.");

        RuleFor(orderItem => orderItem.UnitPrice)
            .NotNull()
            .WithMessage("Unit price is required.");

        RuleFor(orderItem => orderItem.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero."); 
    }
}