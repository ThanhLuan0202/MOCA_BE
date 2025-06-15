using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IPregnancyTrackingRepository
    {

        Task<IEnumerable<PregnancyTracking>> GetAlLPregnancyTrackingAsync(string userId);
        Task<PregnancyTracking> CreatePregnancyTrackingAsync(PregnancyTracking newPr, string userId);
        Task<PregnancyTracking> UpdatePregnancyTrackingAsync(int id, PregnancyTracking updatePr);
        Task<PregnancyTracking> GetPregnancyTrackingByIdAsync(int id);

    }
}
