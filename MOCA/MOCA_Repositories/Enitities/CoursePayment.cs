using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Enitities;

public partial class CoursePayment
{
    public int PaymentId { get; set; }
    public int? OrderId { get; set; }
    public string? PaymentCode { get; set; }
    public string? TransactionIdResponse { get; set; }
    public string? PaymentGateway { get; set; }
    public DateTime? CreatedDate { get; set; }
    public double? Amount { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }

    public OrderCourse OrderCourse { get; set; }
}
