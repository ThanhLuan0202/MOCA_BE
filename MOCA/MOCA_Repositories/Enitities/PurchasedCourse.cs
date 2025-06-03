using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class PurchasedCourse
{
    public int PurchasedId { get; set; }

    public int? CourseId { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }
    
    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
