using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
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
        private readonly MOCAContext _context;
        private  readonly IEmailService _mailService;
        public DoctorProfileService(IDoctorProfileRepository repo, MOCAContext context, IEmailService mailService)
        {
            _repo = repo;
            _context = context;
            _mailService = mailService;
        }

        public async Task<DoctorProfile> CreateDoctorProfileAsync(DoctorProfile newDoctorProfile, string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var userEmail = await _context.Users
    .Where(u => u.UserId == idUser).FirstOrDefaultAsync();


            await _mailService.SendVerificationEmailToDoctorAsync(userEmail.Email, newDoctorProfile.FullName);
            return  await _repo.CreateDoctorProfileAsync(newDoctorProfile, userId);
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
