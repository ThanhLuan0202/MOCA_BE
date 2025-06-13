using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseCartDTO;

namespace MOCA_Repositories.Repositories
{
    public class CourseCartRepository : ICourseCartRepository
    {
        private readonly IMemoryCache _cache;
        private const string CartPrefix = "Cart_";
        public CourseCartRepository(IMemoryCache cache)
        {
            _cache = cache;
        }
        public CartModel? GetCart(int userId) =>
        _cache.Get<CartModel>($"{CartPrefix}{userId}");

        public void SaveCart(CartModel cart) =>
            _cache.Set($"{CartPrefix}{cart.UserId}", cart, TimeSpan.FromHours(1));

        public void RemoveCart(int userId) =>
            _cache.Remove($"{CartPrefix}{userId}");
    }
}
