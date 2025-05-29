using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IMomProfileService
    {

        Task<IEnumerable<MomProfile>> GetAlLPackageAsync();
        Task<MomProfile> CreateMomProfileAsync(CreateMomProfileModel newMomPr, String userId);
        Task<MomProfile> UpdateMomProfileAsync(int id, UpdateMomProfileModel updateMomPr);

        Task<MomProfile> GetMomProfileByIdAsync(int id);

    }
}
