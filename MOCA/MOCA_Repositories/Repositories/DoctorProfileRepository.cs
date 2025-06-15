using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class DoctorProfileRepository : GenericRepository<DoctorProfile>, IDoctorProfileRepository
    {
        private readonly MOCAContext _context;

        public DoctorProfileRepository(MOCAContext context)
        {
            _context = context;
        }

        public async Task<DoctorProfile> ConfirmDoctor(int id)
        {
            var checkDoc = await _context.DoctorProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.DoctorId == id);

            if (checkDoc == null)
            {
                throw new Exception($"Doctor profile {id} is not exist!");
            }
            checkDoc.Status = "Active";
            _context.Entry(checkDoc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return checkDoc;
        }

        public async Task<DoctorProfile> CreateDoctorProfileAsync(DoctorProfile newDoctorProfile, string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var newDoctor = new DoctorProfile
            {
                UserId = idUser,
                FullName = newDoctorProfile.FullName,
                Specialization = newDoctorProfile.Specialization,
                Description = newDoctorProfile.Description,
                Status = "Pending"
            };

            await _context.AddAsync(newDoctor);
            await _context.SaveChangesAsync();

            return newDoctor;
        }

        public async Task<DoctorProfile> DeleteDoctorProfileAsync(int id)
        {
            var checkDoc = await _context.DoctorProfiles.FirstOrDefaultAsync(x => x.DoctorId == id && x.Status.Equals("Active"));

            if (checkDoc == null)
            {
                throw new Exception($"Doctor profile {id} is not exist!");
            }

            checkDoc.Status = "Inactive";

            await _context.SaveChangesAsync();

            return checkDoc;

        }

        public async Task<IEnumerable<DoctorProfile>> GetAlLDoctorProfileAsync()
        {

            var query = await _context.DoctorProfiles.Include(x => x.User).ToListAsync();

            return query;

        }

        public async Task<DoctorProfile> GetDoctorProfileByIdAsync(int id)
        {
            var checkDoc = await _context.DoctorProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.DoctorId == id && x.Status.Equals("Active"));

            if (checkDoc == null)
            {
                throw new Exception($"Doctor profile {id} is not exist!");
            }

            return checkDoc;
        }

        public async Task<DoctorProfile> GetDoctorProfileByUserIdAsync(string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");

            }

            var checkDoctor = await _context.DoctorProfiles.Include(x => x.User).FirstOrDefaultAsync(c => c.UserId == idUser);

            return checkDoctor;

        }

        public async Task<DoctorProfile> UpdateDoctorProfileAsync(int id, DoctorProfile updateDoctorProfile)
        {
            var checkDoc = await _context.DoctorProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.DoctorId == id && x.Status.Equals("Active"));

            if (checkDoc == null)
            {
                throw new Exception($"Doctor profile {id} is not exist!");
            }

            checkDoc.FullName = updateDoctorProfile.FullName;
            checkDoc.Specialization = updateDoctorProfile.Specialization;
            checkDoc.Description = updateDoctorProfile.Description;
            
            _context.Entry(checkDoc).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return checkDoc;
        }
    }
}
