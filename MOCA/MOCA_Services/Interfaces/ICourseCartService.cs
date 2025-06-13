using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Models.CourseCartDTO;

namespace MOCA_Services.Interfaces
{
    public interface ICourseCartService
    {
        Task<CartModel> AddToCart(int userId, int courseId);
        CartModel? GetCart(int userId);
        bool RemoveFromCart(int userId, int courseId);
        void ClearCart(int userId);
        Task<CartModel> ApplyDiscountAsync(int userId, int discountId);
        Task<int> CheckoutAsync(int userId, string paymentMethod);
    }
}
