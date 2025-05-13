using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="UpdateOrderHandler"/> class.
/// </summary>
public class UpdateOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    private readonly UpdateOrderHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateOrderHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public UpdateOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _orderService = Substitute.For<IOrderService>();
        _mapper = Substitute.For<IMapper>();
        _handler = new UpdateOrderHandler(_mapper, _orderRepository, _orderService);
    }

    /// <summary>
    /// Tests that a valid order update request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When updating order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = UpdateOrderHandlerTestData.GenerateValidCommand();
        var order = new Order
        {
            Id = Guid.NewGuid()
        };

        var result = new UpdateOrderResult
        {
            Id = order.Id,
        };

        _mapper.Map<Order>(command).Returns(order);
        _mapper.Map<UpdateOrderResult>(order).Returns(result);

        _orderRepository.UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        var updateOrderResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateOrderResult.Should().NotBeNull();
        updateOrderResult.Id.Should().Be(order.Id);
        _orderService.Received(1).ApplyDiscounts(Arg.Any<List<OrderItem>>());
        await _orderRepository.Received(1).UpdateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid order update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid order data When updating order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateOrderCommand(); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
        _orderService.Received(0).ApplyDiscounts(Arg.Any<List<OrderItem>>());
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to order entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var command = UpdateOrderHandlerTestData.GenerateValidCommand();
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Number = command.Number,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName
        };

        _mapper.Map<Order>(command).Returns(order);
        _mapper.Map<UpdateOrderResult>(order).Returns(new UpdateOrderResult{Id = order.Id});
        _orderRepository.CreateAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _orderService.Received(1).ApplyDiscounts(Arg.Any<List<OrderItem>>());
        _mapper.Received(1).Map<Order>(Arg.Is<UpdateOrderCommand>(c =>
            c.Number == command.Number &&
            c.CustomerId == command.CustomerId &&
            c.CustomerName == command.CustomerName &&
            c.BranchId == command.BranchId &&
            c.BranchName == command.BranchName));
    }
}
