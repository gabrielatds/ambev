namespace Ambev.DeveloperEvaluation.Domain.Services.Strategies;

public class NoDiscountStrategy : IDiscountStrategy
{
    public bool CanApply(int quantity) => quantity < 4;
    public decimal CalculateDiscount(int quantity, decimal unitPrice) => 0m;
}