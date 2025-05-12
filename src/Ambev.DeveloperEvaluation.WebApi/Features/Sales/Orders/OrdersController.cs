using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders.CreateOrder;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Orders;

/// <summary>
/// Controller for managing orders operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of OrdersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public OrdersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new order
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created order details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateOrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
        {
            Success = true,
            Message = "Order created successfully",
            Data = _mapper.Map<CreateOrderResponse>(response)
        });
    }
}
