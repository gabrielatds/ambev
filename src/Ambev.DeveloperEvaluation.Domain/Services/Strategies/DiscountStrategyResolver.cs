using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Services.Strategies;

public class DiscountStrategyResolver
{
    private readonly IEnumerable<IDiscountStrategy> _strategies;

    public DiscountStrategyResolver()
    {
        _strategies = new List<IDiscountStrategy>
        {
            new NoDiscountStrategy(),
            new TenPercentDiscountStrategy(),
            new TwentyPercentDiscountStrategy()
        };
    }

    public IDiscountStrategy Resolve(int quantity)
    {
        var strategy = _strategies.Single(strategy => strategy.CanApply(quantity));
        if (strategy == null)
            throw new ValidationException("Invalid quantity. Max allowed: 20.");
        return strategy;
    }
}