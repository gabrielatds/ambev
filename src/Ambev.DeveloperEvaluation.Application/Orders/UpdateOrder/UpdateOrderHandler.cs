using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UpdateOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="orderService">The Order domain service</param>
    public UpdateOrderHandler(IMapper mapper, IOrderRepository orderRepository, IOrderService orderService)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderService = orderService;
    }

    /// <summary>
    /// Handles the UpdateOrderCommand request
    /// </summary>
    /// <param name="command">The UpdateOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated order details</returns>
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        Log.Information("Executing Handle for UpdateOrderCommand {command}", @command);
        
        await ValidateCommandAsync(command, cancellationToken);
        
        var order = _mapper.Map<Order>(command);
        order.SetCurrentDate();

        _orderService.ApplyDiscounts(order.Items);
        
        Log.Information("Updating order {order}", @order);
        var updatedOrder = await _orderRepository.UpdateAsync(order, cancellationToken);
        var result = _mapper.Map<UpdateOrderResult>(updatedOrder);
        
        Log.Information($"Order updated successfully");
        return result;
    }

    private async Task ValidateCommandAsync(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            Log.Error("Invalid order. Errors: {errors}", @validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }
    }
}
