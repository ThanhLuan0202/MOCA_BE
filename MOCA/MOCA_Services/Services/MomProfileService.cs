using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.MomProfileDTO;
using MOCA_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Services
{
    public class MomProfileService : IMomProfileService
    {
        private readonly IMomProfileRepository _repo;

        public MomProfileService(IMomProfileRepository repo)
        {
            _repo = repo;
        }


        public Task<MomProfile> CreateMomProfileAsync(CreateMomProfileModel newMomPr, String userId)
        {
            return _repo.CreateMomProfileAsync(newMomPr, userId);
        }

      

        public Task<IEnumerable<MomProfile>> GetAlLPackageAsync()
        {
            return _repo.GetAlLPackageAsync();
        }

        public Task<MomProfile> GetMomProfileByIdAsync(int id)
        {
            return _repo.GetMomProfileByIdAsync(id);
        }

        public Task<MomProfile> UpdateMomProfileAsync(int id, UpdateMomProfileModel updateMomPr)
        {
            return _repo.UpdateMomProfileAsync(id, updateMomPr);
        }
    }
}
