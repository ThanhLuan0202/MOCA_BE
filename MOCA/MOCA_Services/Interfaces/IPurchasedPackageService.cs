using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPurchasedPackageService
    {
        Task<List<PurchasePackage>> GetAllAsync();
        Task<PurchasePackage?> GetByIdAsync(int id);
        Task<PurchasePackage> AddAsync(string userId, PurchasePackage create);
        Task<PurchasePackage> DeleteAsync(int id);
        Task<List<int>> GetEnrolledPurchasePackageIdsByUserNameAsync(string userId);
    }
}
