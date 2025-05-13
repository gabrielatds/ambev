using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.Sales.TestData
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class OrderItemTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid OrderItem entities.
        /// The generated users will have valid:
        /// - ProductId (random unique GUID identifier)
        /// - ProductTitle (random string)
        /// - UnitPrice (random Money instance)
        /// - Quantity (random number between 1 - 200)
        /// </summary>
        private static readonly Faker<OrderItem> OrderItemFaker = new Faker<OrderItem>()
            .RuleFor(i => i.ProductId, f => Guid.NewGuid())
            .RuleFor(i => i.ProductTitle, f => f.Commerce.ProductName())
            .RuleFor(i => i.UnitPrice, f => new Money(10, "BRL"))
            .RuleFor(i => i.Quantity, f => 5)
            .RuleFor(i => i.Discount, f => new Money(10, "BRL"))
            .RuleFor(i => i.TotalAmount, f => new Money(45, "BRL"));

        /// <summary>
        /// Generates a valid OrderItem entity with randomized data.
        /// The generated user will have all properties populated with valid values
        /// that meet the system's validation requirements.
        /// </summary>
        /// <returns>A valid User entity with randomly generated data.</returns>
        public static OrderItem GenerateValidOrderItem()
        {
            return OrderItemFaker.Generate();
        }

        /// <summary>
        /// Generates a name that exceeds the maximum length limit.
        /// The generated username will:
        /// - Be longer than 100 characters
        /// This is useful for testing username length validation error cases.
        /// </summary>
        /// <returns>A name that exceeds the maximum length limit.</returns>
        public static string GenerateLongName()
        {
            return new Faker().Random.String2(101);
        }
    }
}