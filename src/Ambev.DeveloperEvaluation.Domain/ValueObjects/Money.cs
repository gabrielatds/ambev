namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;

    public Money(decimal amount, string currency)
    {
        Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
        Currency = currency.ToUpperInvariant();
    }

    public Money()
    {
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        if (Amount < other.Amount)
            throw new InvalidOperationException("Resulting amount cannot be negative");
        
        return new Money(Amount - other.Amount, Currency);
    }

    public Money Multiply(int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Multiplier cannot be negative", nameof(multiplier));
        return new Money(Amount * multiplier, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Cannot operate on Money with different currencies");
    }

    public override string ToString() => $"{Currency} {Amount:N2}";

    public override bool Equals(object obj) => Equals(obj as Money);

    public bool Equals(Money other) =>
        other != null &&
        Amount == other.Amount &&
        Currency == other.Currency;

    public override int GetHashCode() => HashCode.Combine(Amount, Currency);
}