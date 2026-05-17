namespace ValtiShop.Domain.Entities;

public class Issuer
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? ContactEmail { get; private set; }
    public string? PhysicalAddress { get; private set; }

    private readonly List<License> _licenses = [];
    public IReadOnlyCollection<License> Licenses => _licenses.AsReadOnly();

    private Issuer() { Name = null!; }

    public Issuer(string name, string? description, string? contactPhone, string? contactEmail, string? physicalAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
        Description = description;
        ContactPhone = contactPhone;
        ContactEmail = contactEmail;
        PhysicalAddress = physicalAddress;
    }

    public void UpdateContact(string? phone, string? email)
    {
        ContactPhone = phone;
        ContactEmail = email;
    }

    public void UpdateAddress(string? address)
    {
        PhysicalAddress = address;
    }

    public License IssueLicense(string name, string? description, DateTime validFrom, DateTime validTo)
    {
        var license = new License(name, description, validFrom, validTo, this);
        _licenses.Add(license);
        return license;
    }
}
