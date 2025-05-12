using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services.Strategies;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Serilog;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class OrderService : IOrderService
{
    private readonly DiscountStrategyResolver _resolver;

    public OrderService()
    {
        _resolver = new DiscountStrategyResolver();
    }

    public decimal GetDiscount(int quantity, decimal unitPrice)
    {
        Log.Information($"Getting discounts for quantity {quantity}, unitPrice {unitPrice}");

        if (quantity > 20)
        {
            Log.Error($"Cannot purchase more than 20 items.");
            throw new ValidationException("Cannot purchase more than 20 items.");
        }
        
        var strategy = _resolver.Resolve(quantity);
        return strategy.CalculateDiscount(quantity, unitPrice);
    }
    
    public void ApplyDiscounts(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            var discount = GetDiscount(item.Quantity, item.UnitPrice.Amount);
            item.ApplyDiscount(new Money(discount, item.UnitPrice.Currency));
            item.SetTotalAmount();
        }
    }
}