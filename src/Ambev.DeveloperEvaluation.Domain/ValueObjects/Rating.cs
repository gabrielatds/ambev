namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed class Rating : IEquatable<Rating>
{
    public double Rate { get; }
    public int Count { get; }

    public static readonly Rating None = new(0.0, 0);

    public Rating(double rate, int count)
    {
        if (rate < 0 || rate > 5)
            throw new ArgumentOutOfRangeException(nameof(rate), "Rate must be between 0 and 5.");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");

        Rate = Math.Round(rate, 2); // Round for consistency
        Count = count;
    }

    public override string ToString() => $"{Rate} stars ({Count} ratings)";

    public override bool Equals(object obj) => Equals(obj as Rating);

    public bool Equals(Rating other) =>
        other != null &&
        Rate == other.Rate &&
        Count == other.Count;

    public override int GetHashCode() => HashCode.Combine(Rate, Count);
}