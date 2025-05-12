using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IOrderService
{
    decimal GetDiscount(int quantity, decimal unitPrice);
    void ApplyDiscounts(List<OrderItem> items);
}
