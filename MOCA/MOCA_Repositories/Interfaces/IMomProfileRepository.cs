using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using MOCA_Repositories.Models.PackageDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IMomProfileRepository
    {

        Task<IEnumerable<MomProfile>> GetAlLPackageAsync();
        Task<MomProfile> CreateMomProfileAsync(CreateMomProfileModel newMomPr, String userId);
        Task<MomProfile> UpdateMomProfileAsync(int id, UpdateMomProfileModel updateMomPr);

        Task<MomProfile> GetMomProfileByIdAsync(int id);
    }
}
