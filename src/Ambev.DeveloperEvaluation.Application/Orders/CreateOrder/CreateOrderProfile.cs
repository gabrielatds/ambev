using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Profile for mapping between Order entity and CreateOrderResponse
/// </summary>
public class CreateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateOrder operation
    /// </summary>
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderResult>();
        
        CreateMap<OrderItemCommand, OrderItem>();
        CreateMap<OrderItem, OrdemItemResult>();
    }
}
