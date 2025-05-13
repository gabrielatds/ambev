using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

/// <summary>
/// Profile for mapping between Order entity and UpdateOrderResponse
/// </summary>
public class UpdateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateOrder operation
    /// </summary>
    public UpdateOrderProfile()
    {
        CreateMap<UpdateOrderCommand, Order>();
        CreateMap<Order, UpdateOrderResult>();
        
        CreateMap<UpdateOrderItemCommand, OrderItem>();
        CreateMap<OrderItem, UpdateOrderItemResult>();
    }
}
