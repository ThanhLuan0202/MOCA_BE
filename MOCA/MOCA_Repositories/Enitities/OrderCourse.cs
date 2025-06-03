using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Enitities;

public partial class OrderCourse
{
    public int OrderId { get; set; }
    public int? UserId { get; set; }
    public int? DiscountId { get; set; }
    public decimal? OrderPrice { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedDate { get; set; }

    public User? User { get; set; }
    public Discount? Discount { get; set; }
    public ICollection<OrderCourseDetail> OrderCourseDetails { get; set; } = new List<OrderCourseDetail>();
    public ICollection<CoursePayment> CoursePayments { get; set; } = new List<CoursePayment>();
}


