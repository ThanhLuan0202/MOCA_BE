using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Package
{
    public int PackageId { get; set; }

    public string? PackageName { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<PurchasePackage> PurchasePackages { get; set; } = new List<PurchasePackage>();
}
