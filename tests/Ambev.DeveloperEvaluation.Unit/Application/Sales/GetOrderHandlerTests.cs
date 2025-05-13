using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="GetOrderHandler"/> class.
/// </summary>
public class GetOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly GetOrderHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetOrderHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public GetOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetOrderHandler(_orderRepository, _mapper);
    }

    /// <summary>
    /// Tests that a valid order creation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When retrieving order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new GetOrderCommand(Guid.NewGuid());
        var order = new Order
        {
            Id = Guid.NewGuid()
        };

        var result = new GetOrderResult
        {
            Id = order.Id
        };

        _mapper.Map<GetOrderResult>(order).Returns(result);

        _orderRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        var getOrderResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        getOrderResult.Should().NotBeNull();
        getOrderResult.Id.Should().Be(order.Id);
        await _orderRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid order creation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid order data When retrieving order. Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new GetOrderCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to order entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var command = new GetOrderCommand(Guid.NewGuid());
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Number = 123,
            CustomerId = Guid.NewGuid(),
            CustomerName = "Customer Name",
            BranchId = Guid.NewGuid(),
            BranchName = "Branch Name"
        };

        var result = new GetOrderResult
        {
            Id = order.Id,
            Number = order.Number,
            CustomerId = order.CustomerId,
            CustomerName = order.CustomerName,
            BranchId = order.BranchId,
            BranchName = order.BranchName
        };

        _mapper.Map<GetOrderResult>(order).Returns(result);
        _orderRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(order);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<GetOrderResult>(Arg.Is<Order>(c =>
            c.Number == result.Number &&
            c.CustomerId == result.CustomerId &&
            c.CustomerName == result.CustomerName &&
            c.BranchId == result.BranchId &&
            c.BranchName == result.BranchName));
    }
    
    /// <summary>
    /// Tests that an invalid order retrieving request throws a validation exception due to key not found.
    /// </summary>
    [Fact(DisplayName = "Given invalid order data When retrieving order Then throws validation exception due duplicated order number")]
    public async Task Handle_InvalidRequest_ThrowsKeyNotFoundException()
    {
        // Given
        var command = new GetOrderCommand(Guid.NewGuid());
        _orderRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
