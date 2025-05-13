using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
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
/// Contains unit tests for the <see cref="DeleteOrderHandler"/> class.
/// </summary>
public class DeleteOrderHandlerTests
{
    private readonly IOrderRepository _orderRepository;
    private readonly DeleteOrderHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteOrderHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public DeleteOrderHandlerTests()
    {
        _orderRepository = Substitute.For<IOrderRepository>();
        _handler = new DeleteOrderHandler(_orderRepository);
    }

    /// <summary>
    /// Tests that a valid order delete request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When delete order Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = new DeleteOrderCommand(Guid.NewGuid());
        var order = new Order
        {
            Id = Guid.NewGuid()
        };

        _orderRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);

        // When
        var deleteOrderResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        deleteOrderResult.Should().NotBeNull();
        deleteOrderResult.Success.Should().Be(true);
        await _orderRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid order delete request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid order data When creating order Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new DeleteOrderCommand(Guid.Empty); // Empty command will fail validation

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    /// <summary>
    /// Tests that a valid order delete request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid order data When delete order Then returns success response")]
    public async Task Handle_InvalidRequest_ReturnsKeyNotFoundException()
    {
        // Given
        var command = new DeleteOrderCommand(Guid.NewGuid());
        var order = new Order
        {
            Id = Guid.NewGuid()
        };

        _orderRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(false);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
