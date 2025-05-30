using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Course? Course { get; set; }
}
