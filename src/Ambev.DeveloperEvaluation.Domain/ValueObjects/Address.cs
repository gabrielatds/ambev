namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public sealed class Address : IEquatable<Address>
{
    public string Street { get; }
    public string Number { get; }
    public string Complement { get; }
    public string Neighborhood { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    public Address(
        string street,
        string number,
        string complement,
        string neighborhood,
        string city,
        string state,
        string postalCode,
        string country)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public Address()
    {
    }

    public override string ToString()
    {
        return $"{Street}, {Number} {Complement}, {Neighborhood}, {City} - {State}, {PostalCode}, {Country}";
    }

    public override bool Equals(object obj) => Equals(obj as Address);

    public bool Equals(Address other)
    {
        if (other is null) return false;
        return Street == other.Street &&
               Number == other.Number &&
               Complement == other.Complement &&
               Neighborhood == other.Neighborhood &&
               City == other.City &&
               State == other.State &&
               PostalCode == other.PostalCode &&
               Country == other.Country;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Street, Number, Complement, Neighborhood, City, State, PostalCode, Country);
    }
}