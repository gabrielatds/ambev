using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.CreateOrder;

/// <summary>
/// Profile for mapping between Application and API CreateOrder responses
/// </summary>
public class CreateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateOrder feature
    /// </summary>
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderCommand>();
        CreateMap<CreateOrderResult, CreateOrderResponse>();
        
        CreateMap<OrderItemRequest, OrderItemCommand>();
        CreateMap<OrderItemResult, OrderItemResponse>();
    }
}
