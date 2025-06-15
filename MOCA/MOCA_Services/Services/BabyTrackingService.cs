using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class BabyTrackingService : IBabyTrackingService
    {
        private readonly IBabyTrackingRepository _repo;

        public BabyTrackingService(IBabyTrackingRepository repo)
        {
            _repo = repo;
        }

        public Task<BabyTracking> CreateBabyTrackingAsync(BabyTracking newBb, string userId)
        {
            return _repo.CreateBabyTrackingAsync(newBb, userId);
        }

        public Task<IEnumerable<BabyTracking>> GetAlLBabyTrackingAsync(string userId)
        {
            return _repo.GetAlLBabyTrackingAsync(userId);
        }

        public Task<BabyTracking> GetBabyTrackingByIdAsync(string id)
        {
            return _repo.GetBabyTrackingByIdAsync(id);
        }

        public Task<BabyTracking> UpdateBabyTrackingAsync(int id, BabyTracking updateBb)
        {
            return _repo.UpdateBabyTrackingAsync(id, updateBb);
        }
    }
}
