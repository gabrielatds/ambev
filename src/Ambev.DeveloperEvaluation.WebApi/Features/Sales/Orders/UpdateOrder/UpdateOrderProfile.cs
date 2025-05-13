using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.UpdateOrder;

/// <summary>
/// Profile for mapping between Application and API CreateOrder responses
/// </summary>
public class UpdateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateOrder feature
    /// </summary>
    public UpdateOrderProfile()
    {
        CreateMap<UpdateOrderRequest, UpdateOrderCommand>();
        CreateMap<UpdateOrderResult, UpdateOrderResponse>();
        
        CreateMap<UpdateOrderItemRequest, UpdateOrderItemCommand>();
        CreateMap<UpdateOrderItemResult, UpdateOrderItemResponse>();
    }
}
