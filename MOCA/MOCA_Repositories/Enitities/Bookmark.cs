using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class Bookmark
{
    public int BookmarkId { get; set; }

    public int? CourseId { get; set; }

    public string? UserName { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Course? Course { get; set; }
}
