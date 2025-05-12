namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed class Rating : IEquatable<Rating>
{
    public double Rate { get; }
    public int Count { get; }

    public static readonly Rating None = new(0.0, 0);

    public Rating(double rate, int count)
    {
        Rate = Math.Round(rate, 2);
        Count = count;
    }

    public Rating()
    {
    }

    public override string ToString() => $"{Rate} stars ({Count} ratings)";

    public override bool Equals(object obj) => Equals(obj as Rating);

    public bool Equals(Rating other) =>
        other != null &&
        Rate == other.Rate &&
        Count == other.Count;

    public override int GetHashCode() => HashCode.Combine(Rate, Count);
}