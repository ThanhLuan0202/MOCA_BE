using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Models.PurchasedCourseDTO
{
    public class UpdatePurchasedCourseModel
    {
        public int? CourseId { get; set; }
        public int? DiscountId { get; set; }
    }
}
