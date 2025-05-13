using Ambev.DeveloperEvaluation.Application.Orders.DeleteOrder;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteOrderHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Order entities.
    /// The generated users will have valid:
    /// - OrderId (random unique GUID identifier)
    /// </summary>
    private static readonly Faker<DeleteOrderCommand> deleteOrderHandlerFaker = new Faker<DeleteOrderCommand>()
        .RuleFor(order => order.Id, f => Guid.NewGuid());

    /// <summary>
    /// Generates a valid Order entity with randomized data.
    /// The generated order will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Order entity with randomly generated data.</returns>
    public static DeleteOrderCommand GenerateValidCommand()
    {
        return deleteOrderHandlerFaker.Generate();
    }
}
