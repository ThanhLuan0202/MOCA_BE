using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Repositories;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class PurchasedPackageService : IPurchasedPackageService
    {
        private readonly IPurchasedPackageRepository _repo;

        public PurchasedPackageService(IPurchasedPackageRepository repo)
        {
            _repo = repo;
        }

        public Task<PurchasePackage> AddAsync(string userId, PurchasePackage create)
        {
            return _repo.AddAsync(userId, create);
        }

        public Task<PurchasePackage> DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }

        public Task<List<PurchasePackage>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<PurchasePackage?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }

        public Task<List<int>> GetEnrolledPurchasePackageIdsByUserNameAsync(string userId)
        {
            return _repo.GetEnrolledPurchasePackageIdsByUserNameAsync(userId);
        }
    }
}
