using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

/// <summary>
/// Handler for processing GetOrderCommand requests
/// </summary>
public class GetOrderHandler : IRequestHandler<GetOrderCommand, GetOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetOrderHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetOrderCommand request
    /// </summary>
    /// <param name="command">The GetOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The order details if found</returns>
    public async Task<GetOrderResult> Handle(GetOrderCommand command, CancellationToken cancellationToken)
    {
        Log.Information("Executing Handle for GetOrderCommand {command}", @command);
        await ValidateCommandAsync(command, cancellationToken);

        var order = await _orderRepository.GetByIdAsync(command.Id, cancellationToken);
        if (order == null)
            throw new KeyNotFoundException($"Order with ID {command.Id} not found");
        
        Log.Information($"Order retrieved successfully");

        return _mapper.Map<GetOrderResult>(order);
    }

    private static async Task ValidateCommandAsync(GetOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetOrderValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
