using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPregnancyTrackingService
    {
        Task<IEnumerable<PregnancyTracking>> GetAlLPregnancyTrackingAsync(string userId);
        Task<PregnancyTracking> CreatePregnancyTrackingAsync(PregnancyTracking newPr, string userId);
        Task<PregnancyTracking> UpdatePregnancyTrackingAsync(int id, PregnancyTracking updatePr);
        Task<IEnumerable<PregnancyTracking>> GetPregnancyTrackingByIdAsync(int id);


    }
}
