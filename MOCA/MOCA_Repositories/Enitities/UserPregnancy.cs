using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class UserPregnancy
{
    public int PregnancyId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BabyTracking> BabyTrackings { get; set; } = new List<BabyTracking>();

    public virtual ICollection<PregnancyTracking> PregnancyTrackings { get; set; } = new List<PregnancyTracking>();

    public virtual User? User { get; set; }
}
