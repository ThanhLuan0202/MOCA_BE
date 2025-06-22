using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IBabyTrackingService
    {
        Task<IEnumerable<BabyTracking>> GetAlLBabyTrackingAsync(string userId);
        Task<BabyTracking> CreateBabyTrackingAsync(BabyTracking newBb, string userId);
        Task<BabyTracking> UpdateBabyTrackingAsync(int id, BabyTracking updateBb);
        Task<IEnumerable<BabyTracking>> GetBabyTrackingByIdAsync(int id);

    }
}
