using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateOrderHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="orderService">The Order domain service</param>
    public CreateOrderHandler(IMapper mapper, IOrderRepository orderRepository, IOrderService orderService)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderService = orderService;
    }

    /// <summary>
    /// Handles the CreateOrderCommand request
    /// </summary>
    /// <param name="command">The CreateOrder command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order details</returns>
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        Log.Information("Executing Handle for CreateOrderCommand {command}", @command);
        
        await ValidateCommandAsync(command, cancellationToken);
        
        var order = _mapper.Map<Order>(command);
        order.SetCurrentDate();

        _orderService.ApplyDiscounts(order.Items);
        
        Log.Information("Creating order {order}", @order);
        var createdOrder = await _orderRepository.CreateAsync(order, cancellationToken);
        var result = _mapper.Map<CreateOrderResult>(createdOrder);
        
        Log.Information($"Order created successfully with Id: {result.Id}");
        return result;
    }

    private async Task ValidateCommandAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            Log.Error("Invalid order. Errors: {errors}", @validationResult.Errors);
            throw new ValidationException(validationResult.Errors);
        }
            
        var existingOrder = await _orderRepository.GetByOrderNumberAsync(command.Number, cancellationToken);
        if (existingOrder != null)
        {
            Log.Error($"Order with number {command.Number} already exists");
            throw new ValidationException($"Order with number {command.Number} already exists");
        }
    }
}
