
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.MomProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class MomProfileRepository : GenericRepository<MomProfile>, IMomProfileRepository
    {
        private readonly MOCAContext _context;


        public MomProfileRepository(MOCAContext context)
        {
            _context = context;
        }

        public async Task<string> CheckMomProfile(int userId)
        {
            

            var check = await _context.MomProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);

            if (check != null)
            {
                return "Is Mom";
            }
            else 
            { 
                return "Is Not Mom";
            }
        }

        public async Task<MomProfile> CreateMomProfileAsync(CreateMomProfileModel newMomPr, String userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var newMom = new MomProfile
            {
                UserId = idUser,
                Address = newMomPr.Address,
                BloodType = newMomPr.BloodType,
                DateOfBirth = newMomPr.DateOfBirth,
                MaritalStatus = newMomPr.MaritalStatus,
                MedicalHistory = newMomPr.MedicalHistory,
            };
            
            await _context.AddAsync(newMom);
            await _context.SaveChangesAsync();

            return newMom;


        }

        

        public async Task<IEnumerable<MomProfile>> GetAlLPackageAsync()
        {
            var query = await _context.MomProfiles.Include(x => x.User).ToListAsync();

            return query;

        }

        public async Task<MomProfile> GetMomProfileByIdAsync(int id)
        {
            var checkMom = await _context.MomProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.MomId == id);

            if (checkMom == null)
            {
                throw new Exception($"Mom profile {id} is not exist!");
            }
            return checkMom;
           
        }

        public async Task<MomProfile> GetMomProfileByUserIdAsync(int id)
        {
            var check = await _context.MomProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id);
            if (check == null)
            {
                throw new Exception($"Mom profile {id} is not exist!");
            }

            return check;
        }

        public async Task<MomProfile> GetMomProfileByUserIdInput(int id)
        {
            var check = await _context.MomProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id);

            if (check == null)
            {
                throw new Exception($"Mom profile {id} is not exist!");
            }

            return check;
        }

        public async Task<MomProfile> UpdateMomProfileAsync(int id, UpdateMomProfileModel updateMomPr)
        {
            var checkMom = await _context.MomProfiles.Include(x => x.User).FirstOrDefaultAsync(x => x.MomId == id);
            if (checkMom == null)
            {
                throw new Exception($"Mom profile {id} is not exist!");
            }

            checkMom.Address = updateMomPr.Address;
            checkMom.DateOfBirth = updateMomPr.DateOfBirth;
            checkMom.MedicalHistory = updateMomPr.MedicalHistory;
            checkMom.BloodType = updateMomPr.BloodType;
            checkMom.MaritalStatus = updateMomPr.MaritalStatus;

            _context.Entry(checkMom).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return checkMom;

        }
    }
}
