using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IDoctorProfileRepository
    {
        Task<IEnumerable<DoctorProfile>> GetAlLDoctorProfileAsync();
        Task<DoctorProfile> CreateDoctorProfileAsync(DoctorProfile newDoctorProfile, String userId);
        Task<DoctorProfile> UpdateDoctorProfileAsync(int id, DoctorProfile updateDoctorProfile);
        Task<DoctorProfile> DeleteDoctorProfileAsync(int id);

        Task<DoctorProfile> GetDoctorProfileByIdAsync(int id);
        Task<DoctorProfile> GetDoctorProfileByUserIdAsync(string userId);


    }
}
