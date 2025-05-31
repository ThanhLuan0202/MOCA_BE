using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class PurchasedCourseServices : IPurchasedCourseServices
    {
        private readonly IPurchasedCourseRepository _purchasedCourseRepository;

        public PurchasedCourseServices(IPurchasedCourseRepository purchaseRepository)
        {
            _purchasedCourseRepository = purchaseRepository;
        }

        public async Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId)
        {
            return await _purchasedCourseRepository.GetEnrolledCourseIdsByUserNameAsync(userId);
        }
    }
}
