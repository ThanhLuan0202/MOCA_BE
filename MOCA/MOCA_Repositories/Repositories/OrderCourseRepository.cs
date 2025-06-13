using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;

namespace MOCA_Repositories.Repositories
{
    public class OrderCourseRepository : IOrderCourseRepository
    {
        private readonly MOCAContext _context;
        public OrderCourseRepository(MOCAContext context)
        {
            _context = context;
        }
        public async Task<int> CreateOrderAsync(OrderCourse order)
        {
            _context.OrderCourses.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<Discount?> GetDiscountByIdAsync(int discountId)
        {
            return await _context.Discounts.FindAsync(discountId);
        }
    }
}
