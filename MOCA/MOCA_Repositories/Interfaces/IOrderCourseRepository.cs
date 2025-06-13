using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;

namespace MOCA_Repositories.Interfaces
{
    public interface IOrderCourseRepository
    {
        Task<int> CreateOrderAsync(OrderCourse order);
        Task<Discount?> GetDiscountByIdAsync(int discountId);
        Task<OrderCourse?> GetByIdWithDetailsAsync(int orderId);
        Task UpdateAsync(OrderCourse order);
    }
}
