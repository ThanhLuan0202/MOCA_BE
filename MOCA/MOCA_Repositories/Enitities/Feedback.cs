using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string? UserName { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreateDate { get; set; }
}
