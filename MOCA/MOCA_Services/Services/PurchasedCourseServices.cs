using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PurchasedCourseDTO;
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

        public async Task<PurchasedCourse> AddAsync(int userId, CreatePurchasedCourseModel create)
        {
            return await _purchasedCourseRepository.AddAsync(userId, create);
        }

        public async Task<PurchasedCourse> DeleteAsync(int id)
        {
            return await _purchasedCourseRepository.DeleteAsync(id);
        }

        public async Task<List<PurchasedCourse>> GetAllAsync()
        {
            return await _purchasedCourseRepository.GetAllAsync();
        }

        public async Task<PurchasedCourse?> GetByIdAsync(int id)
        {
            return await _purchasedCourseRepository.GetByIdAsync(id);
        }

        public async Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId)
        {
            return await _purchasedCourseRepository.GetEnrolledCourseIdsByUserNameAsync(userId);
        }

        public async Task<PurchasedCourse> UpdateAsync(int id, UpdatePurchasedCourseModel update)
        {
            return await _purchasedCourseRepository.UpdateAsync(id, update);
        }
    }
}
