using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PurchasedCourseDTO;

namespace MOCA_Services.Interfaces
{
    public interface IPurchasedCourseServices
    {
        Task<List<PurchasedCourse>> GetAllAsync();
        Task<PurchasedCourse?> GetByIdAsync(int id);
        Task<PurchasedCourse> AddAsync(int userId, CreatePurchasedCourseModel create);
        Task<PurchasedCourse> UpdateAsync(int id, UpdatePurchasedCourseModel update);
        Task<PurchasedCourse> DeleteAsync(int id);
        Task<List<int>> GetEnrolledCourseIdsByUserNameAsync(int userId);
    }
}
