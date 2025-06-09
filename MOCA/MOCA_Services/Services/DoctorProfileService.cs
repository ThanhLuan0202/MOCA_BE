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
    public class DoctorProfileService : IDoctorProfileService
    {
        private readonly IDoctorProfileRepository _repo;

        public DoctorProfileService(IDoctorProfileRepository repo)
        {
            _repo = repo;
        }

        public Task<DoctorProfile> CreateDoctorProfileAsync(DoctorProfile newDoctorProfile, string userId)
        {
            return _repo.CreateDoctorProfileAsync(newDoctorProfile, userId);
        }

        public Task<DoctorProfile> DeleteDoctorProfileAsync(int id)
        {
            return _repo.DeleteDoctorProfileAsync(id);
        }

        public Task<IEnumerable<DoctorProfile>> GetAlLDoctorProfileAsync()
        {
            return _repo.GetAlLDoctorProfileAsync();
        }

        public Task<DoctorProfile> GetDoctorProfileByIdAsync(int id)
        {
            return _repo.GetDoctorProfileByIdAsync(id);
        }

        public Task<DoctorProfile> GetDoctorProfileByUserIdAsync(string userId)
        {
            return _repo.GetDoctorProfileByUserIdAsync(userId);
        }

        public Task<DoctorProfile> UpdateDoctorProfileAsync(int id, DoctorProfile updateDoctorProfile)
        {
            return _repo.UpdateDoctorProfileAsync(id, updateDoctorProfile);
        }
    }
}
