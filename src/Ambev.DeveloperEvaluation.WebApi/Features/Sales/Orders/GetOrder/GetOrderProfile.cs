using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.GetOrder;

/// <summary>
/// Profile for mapping GetOrder feature requests to commands
/// </summary>
public class GetOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetOrder feature
    /// </summary>
    public GetOrderProfile()
    {
        CreateMap<Guid, GetOrderCommand>()
            .ConstructUsing(id => new GetOrderCommand(id));

        CreateMap<GetOrderResult, GetOrderResponse>();
        CreateMap<GetOrderItemResult, GetOrderItemResponse>();
    }
}
