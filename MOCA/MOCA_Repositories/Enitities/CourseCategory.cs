using System;
using System.Collections.Generic;

namespace MOCA_Repositories.Enitities;

public partial class CourseCategory
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
