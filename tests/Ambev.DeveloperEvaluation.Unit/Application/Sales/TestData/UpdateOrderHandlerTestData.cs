using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateOrder;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateOrderHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Order entities.
    /// The generated users will have valid:
    /// - Number (number greater than 0)
    /// - CustomerId (random unique GUID identifier)
    /// - CustomerName (random string)
    /// - BranchId (random unique GUID identifier)
    /// - BranchName (random string)
    /// - Items (random array for UpdateOrderItemCommand)
    /// </summary>
    private static readonly Faker<UpdateOrderCommand> updateOrderHandlerFaker = new Faker<UpdateOrderCommand>()
        .RuleFor(order => order.Number, f => f.Random.Number(1, 99999))
        .RuleFor(order => order.CustomerId, Guid.NewGuid())
        .RuleFor(order => order.CustomerName, f => f.Name.FirstName())
        .RuleFor(order => order.BranchId, f => Guid.NewGuid())
        .RuleFor(order => order.BranchName, f => f.Company.CompanyName())
        .RuleFor(order => order.Items, f => updateOrderItemFaker.Generate(f.Random.Int(1, 5)));
    
    
    /// <summary>
    /// Configures the Faker to generate valid OrderItem entities.
    /// The generated users will have valid:
    /// - ProductId (random unique GUID identifier)
    /// - ProductTitle (random string)
    /// - UnitPrice (random Money instance)
    /// - Quantity (random number between 1 - 200)
    /// </summary>
    private static readonly Faker<UpdateOrderItemCommand> updateOrderItemFaker = new Faker<UpdateOrderItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.ProductTitle, f => f.Commerce.ProductName())
        .RuleFor(i => i.UnitPrice, f => new Money(f.Random.Number(1, 100), "BRL"))
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 200));

    /// <summary>
    /// Generates a valid Order entity with randomized data.
    /// The generated order will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Order entity with randomly generated data.</returns>
    public static UpdateOrderCommand GenerateValidCommand()
    {
        return updateOrderHandlerFaker.Generate();
    }
}
