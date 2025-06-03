using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Enitities;

public partial class OrderCourseDetail
{
    public int OrderDetailId { get; set; }
    public int? OrderId { get; set; }
    public int? CourseId { get; set; }
    public decimal Price { get; set; }

    public OrderCourse? OrderCourse { get; set; }
    public Course? Course { get; set; }
}

