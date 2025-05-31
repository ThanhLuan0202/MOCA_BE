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

        Task<IEnumerable<BabyTracking>> GetAlLBabyTrackingAsync();
        Task<BabyTracking> CreateBabyTrackingAsync(BabyTracking newBb, string userId);
        Task<BabyTracking> UpdateBabyTrackingAsync(int id, BabyTracking updateBb);
        Task<BabyTracking> GetBabyTrackingByIdAsync(int id);

    }
}
