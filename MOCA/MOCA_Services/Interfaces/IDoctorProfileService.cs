using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IDoctorProfileService
    {
        Task<IEnumerable<DoctorProfile>> GetAlLDoctorProfileAsync();
        Task<DoctorProfile> CreateDoctorProfileAsync(DoctorProfile newDoctorProfile, String userId);
        Task<DoctorProfile> UpdateDoctorProfileAsync(int id, DoctorProfile updateDoctorProfile);
        Task<DoctorProfile> DeleteDoctorProfileAsync(int id);
        Task<DoctorProfile> ConfirmDoctor(int id);
        Task<DoctorProfile> GetDoctorProfileByIdAsync(int id);

        Task<DoctorProfile> GetDoctorProfileByUserIdAsync(string userId);

    }
}
