using System;
using System.Collections.Generic;

namespace ValtiShop.Persistence.ScaffoldModels;

public partial class Issuer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public string? PhysicalAddress { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}
