using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Advertisement
{
    public int AdId { get; set; }

    public string? Title { get; set; }

    public string? ImageUrl { get; set; }

    public string? RedirectUrl { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public bool? IsVisible { get; set; }
}
