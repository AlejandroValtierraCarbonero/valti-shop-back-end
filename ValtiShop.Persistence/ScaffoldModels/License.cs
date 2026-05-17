using System;
using System.Collections.Generic;

namespace ValtiShop.Persistence.ScaffoldModels;

public partial class License
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public int IssuerId { get; set; }

    public virtual Issuer Issuer { get; set; } = null!;
}
