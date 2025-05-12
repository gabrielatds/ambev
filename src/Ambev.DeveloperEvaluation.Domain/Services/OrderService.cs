using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services.Strategies;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

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
        if (quantity > 20)
            throw new DomainException("Cannot purchase more than 20 items.");
        
        var strategy = _resolver.Resolve(quantity);
        return strategy.CalculateDiscount(quantity, unitPrice);
    }
    
    public void ApplyDiscounts(List<OrderItem> items)
    {
        foreach (var item in items)
        {
            var discount = GetDiscount(item.Quantity, item.UnitPrice.Amount);
            item.ApplyDiscount(new Money(discount, item.UnitPrice.Currency));
        }
    }
}