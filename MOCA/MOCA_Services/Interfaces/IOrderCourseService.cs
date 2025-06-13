using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;

namespace MOCA_Services.Interfaces
{
    public interface IOrderCourseService
    {
        Task<OrderCourse?> GetByIdWithDetailsAsync(int orderId);
        Task UpdateAsync(OrderCourse order);
    }
}
