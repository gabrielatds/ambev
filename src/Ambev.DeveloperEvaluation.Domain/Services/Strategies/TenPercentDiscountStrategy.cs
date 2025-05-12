namespace Ambev.DeveloperEvaluation.Domain.Services.Strategies;

public class TenPercentDiscountStrategy : IDiscountStrategy
{
    public bool CanApply(int quantity) => quantity >= 4 && quantity < 10;
    public decimal CalculateDiscount(int quantity, decimal unitPrice)
        => quantity * unitPrice * 0.10m;
}