namespace Ambev.DeveloperEvaluation.Domain.Services.Strategies;

public interface IDiscountStrategy
{
    bool CanApply(int quantity);
    decimal CalculateDiscount(int quantity, decimal unitPrice);
}