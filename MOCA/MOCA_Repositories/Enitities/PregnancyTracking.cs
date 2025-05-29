using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class PregnancyTracking
{
    public int TrackingId { get; set; }

    public int? PregnancyId { get; set; }

    public DateOnly? TrackingDate { get; set; }

    public int? WeekNumber { get; set; }

    public decimal? Weight { get; set; }

    public decimal? BellySize { get; set; }

    public string? BloodPressure { get; set; }

    public virtual UserPregnancy? Pregnancy { get; set; }
}
