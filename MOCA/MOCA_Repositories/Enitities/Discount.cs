using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public decimal? Value { get; set; }

    public int? MaxUsage { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<PurchasePackage> PurchasePackages { get; set; } = new List<PurchasePackage>();

    public virtual ICollection<PurchasedCourse> PurchasedCourses { get; set; } = new List<PurchasedCourse>();
}
