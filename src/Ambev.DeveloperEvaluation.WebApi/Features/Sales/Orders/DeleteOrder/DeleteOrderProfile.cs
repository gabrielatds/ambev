using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.DeleteOrder;

/// <summary>
/// Profile for mapping DeleteOrder feature requests to commands
/// </summary>
public class DeleteOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for DeleteOrder feature
    /// </summary>
    public DeleteOrderProfile()
    {
        CreateMap<Guid, DeleteOrderCommand>()
            .ConstructUsing(id => new DeleteOrderCommand(id));
    }
}
