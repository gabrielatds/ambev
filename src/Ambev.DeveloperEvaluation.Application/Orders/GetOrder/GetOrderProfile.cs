using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Profile for mapping between Order entity and GetOrderResponse
/// </summary>
public class GetOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetOrder operation
    /// </summary>
    public GetOrderProfile()
    {
        CreateMap<Order, GetOrderResult>();
        CreateMap<OrderItem, GetOrderItemResult>();
    }
}
