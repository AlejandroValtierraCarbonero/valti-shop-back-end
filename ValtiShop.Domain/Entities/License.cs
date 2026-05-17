namespace ValtiShop.Domain.Entities;

public class License
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime ValidTo { get; private set; }
    public int IssuerId { get; private set; }
    public Issuer Issuer { get; private set; } = null!;

    private License() { Name = null!; }

    public License(string name, string? description, DateTime validFrom, DateTime validTo, Issuer issuer)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (validFrom >= validTo)
            throw new ArgumentException("ValidFrom must be before ValidTo");

        Name = name;
        Description = description;
        ValidFrom = validFrom;
        ValidTo = validTo;
        Issuer = issuer;
        IssuerId = issuer.Id;
    }

    public bool IsValidOn(DateTime date) => date >= ValidFrom && date <= ValidTo;

    public void ExtendValidity(DateTime newValidTo)
    {
        if (newValidTo <= ValidTo)
            throw new ArgumentException("New validity end must be after current end");
        ValidTo = newValidTo;
    }
}
