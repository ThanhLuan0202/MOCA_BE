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
    public class PregnancyTrackingService : IPregnancyTrackingService
    {
        private readonly IPregnancyTrackingRepository _repo;

        public PregnancyTrackingService(IPregnancyTrackingRepository repo)
        {
            _repo = repo;
        }

        public Task<PregnancyTracking> CreatePregnancyTrackingAsync(PregnancyTracking newPr, string userId)
        {
            return _repo.CreatePregnancyTrackingAsync(newPr, userId);
        }

        public Task<IEnumerable<PregnancyTracking>> GetAlLPregnancyTrackingAsync(string userId)
        {
            return _repo.GetAlLPregnancyTrackingAsync(userId);
        }

        public Task<PregnancyTracking> GetPregnancyTrackingByIdAsync(int id)
        {
            return _repo.GetPregnancyTrackingByIdAsync(id);
        }

        public Task<PregnancyTracking> UpdatePregnancyTrackingAsync(int id, PregnancyTracking updatePr)
        {
            return _repo.UpdatePregnancyTrackingAsync(id, updatePr);
        }
    }
}
