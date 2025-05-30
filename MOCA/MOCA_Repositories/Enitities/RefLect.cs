using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class RefLect
{
    public int RefLectId { get; set; }

    public int? UserId { get; set; }

    public int? TextReport { get; set; }

    public string? ImageReport { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? Status { get; set; }

    public virtual User? User { get; set; }
}
