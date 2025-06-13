using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Enums;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseCartDTO;
using MOCA_Repositories.Repositories;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CourseCartService : ICourseCartService
    {
        private readonly ICourseCartRepository _courseCartRepository;
        private readonly IOrderCourseRepository _orderCourseRepository;
        private readonly ICourseRepository _courseRepository; 
        private readonly IPurchasedCourseRepository _purchasedCourseRepository;

        public CourseCartService(ICourseCartRepository courseCartRepository, IOrderCourseRepository orderCourseRepository, ICourseRepository courseRepository, IPurchasedCourseRepository purchasedCourseRepository) 
        {
            _courseCartRepository = courseCartRepository;
            _orderCourseRepository = orderCourseRepository;
            _courseRepository = courseRepository;
            _purchasedCourseRepository = purchasedCourseRepository;
        }

        public async Task<CartModel> AddToCart(int userId, int courseId)
        {
            if (await _purchasedCourseRepository.HasUserPurchasedCourse(userId, courseId))
                throw new InvalidOperationException("You have already purchased this course.");

            var cart = _courseCartRepository.GetCart(userId) ?? new CartModel { UserId = userId };

            if (!cart.Items.Any(x => x.CourseId == courseId))
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                    throw new KeyNotFoundException("Course not found.");

                var newItem = new CartItemModel
                {
                    CourseId = courseId,
                    Price = course.Price ?? 0
                };

                cart.Items.Add(newItem);
                _courseCartRepository.SaveCart(cart);
            }

            return cart;
        }


        public async Task<CartModel> ApplyDiscountAsync(int userId, int discountId)
        {
            var cart = _courseCartRepository.GetCart(userId)
                ?? throw new InvalidOperationException("Cart not found.");

            var discount = await _orderCourseRepository.GetDiscountByIdAsync(discountId)
                ?? throw new KeyNotFoundException("Discount not found.");

            if (discount.IsActive != true)
                throw new InvalidOperationException("Discount is inactive.");

            var now = DateTime.UtcNow;
            if (discount.StartDate != null && discount.StartDate > now)
                throw new InvalidOperationException("Discount is not yet active.");

            if (discount.EndDate != null && discount.EndDate < now)
                throw new InvalidOperationException("Discount has expired.");

            var total = cart.Items.Sum(i => i.Price);
            decimal discountValue = 0;

            switch (discount.DiscountType)
            {
                case DiscountType.Percent:
                    if (discount.Value is null || discount.Value <= 0 || discount.Value > 100)
                        throw new InvalidOperationException("Invalid percent discount value.");
                    discountValue = total * discount.Value.Value / 100;
                    break;

                case DiscountType.Amount:
                    if (discount.Value is null || discount.Value <= 0)
                        throw new InvalidOperationException("Invalid amount discount value.");
                    discountValue = discount.Value.Value;
                    break;

                default:
                    throw new InvalidOperationException("Unsupported discount type.");
            }

            if (discountValue > total)
                discountValue = total;

            cart.DiscountId = discountId;
            cart.DiscountValue = discountValue;

            _courseCartRepository.SaveCart(cart);
            return cart;
        }


        public async Task<int> CheckoutAsync(int userId, string paymentMethod)
        {
            var cart = _courseCartRepository.GetCart(userId)
                ?? throw new InvalidOperationException("Cart not found.");

            var total = cart.Items.Sum(i => i.Price) - cart.DiscountValue;

            var order = new OrderCourse
            {
                UserId = userId,
                DiscountId = cart.DiscountId,
                OrderPrice = total,
                PaymentMethod = paymentMethod,
                Status = "Pending",
                CreatedDate = DateTime.Now,
                OrderCourseDetails = cart.Items.Select(item => new OrderCourseDetail
                {
                    CourseId = item.CourseId,
                    Price = item.Price
                }).ToList()
            };

            var orderId = await _orderCourseRepository.CreateOrderAsync(order);
            _courseCartRepository.RemoveCart(userId);
            return orderId;
        }

        public void ClearCart(int userId) => _courseCartRepository.RemoveCart(userId);

        public CartModel? GetCart(int userId) => _courseCartRepository.GetCart(userId);

        public bool RemoveFromCart(int userId, int courseId)
        {
            var cart = _courseCartRepository.GetCart(userId);
            if (cart == null) return false;

            var removed = cart.Items.RemoveAll(x => x.CourseId == courseId) > 0;
            _courseCartRepository.SaveCart(cart);
            return removed;
        }
    }
}
