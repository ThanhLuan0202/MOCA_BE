using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IBabyTrackingRepository
    {

        Task<IEnumerable<BabyTracking>> GetAlLBabyTrackingAsync(string userId);
        Task<BabyTracking> CreateBabyTrackingAsync(BabyTracking newBb, string userId);
        Task<BabyTracking> UpdateBabyTrackingAsync(int id, BabyTracking updateBb);
        Task<IEnumerable<BabyTracking>> GetBabyTrackingByIdAsync(int id);

        Task<BabyTracking> GetBabyTrackingByUserId(int id);


    }
}
