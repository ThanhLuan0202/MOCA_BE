using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PurchasedCourseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IPurchasedPackageRepository
    {
        Task<List<PurchasePackage>> GetAllAsync();
        Task<PurchasePackage?> GetByIdAsync(int id);
        Task<PurchasePackage> AddAsync(string userId, PurchasePackage create);
        Task<PurchasePackage> DeleteAsync(int id);
        Task<List<int>> GetEnrolledPurchasePackageIdsByUserNameAsync(string userId);


    }
}
