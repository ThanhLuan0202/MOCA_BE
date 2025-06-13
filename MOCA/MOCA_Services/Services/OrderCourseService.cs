using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class OrderCourseService : IOrderCourseService
    {
        private readonly IOrderCourseRepository _orderCourseRepository;
        public OrderCourseService(IOrderCourseRepository orderCourseRepository)
        {
            _orderCourseRepository = orderCourseRepository;
        }

        public async Task<OrderCourse?> GetByIdWithDetailsAsync(int orderId)
        {
            return await _orderCourseRepository.GetByIdWithDetailsAsync(orderId);
        }

        public async Task UpdateAsync(OrderCourse order)
        {
             await _orderCourseRepository.UpdateAsync(order);
        }
    }
}
