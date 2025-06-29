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
    public class PregnancyTrackingRepository : GenericRepository<PregnancyTracking>, IPregnancyTrackingRepository
    {
        private readonly MOCAContext _context;
        private readonly IMomProfileRepository _profileRepository;
        private readonly IUserPregnanciesRepository _userPregnanciesRepository;

        public PregnancyTrackingRepository(MOCAContext context, IMomProfileRepository profileRepository, IUserPregnanciesRepository userPregnanciesRepository)
        {
            _context = context;
            _profileRepository = profileRepository;
            _userPregnanciesRepository = userPregnanciesRepository;
        }


        public async Task<PregnancyTracking> CreatePregnancyTrackingAsync(PregnancyTracking newPr, string userId)
        {

            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var checkMom = await _profileRepository.GetMomProfileByUserIdAsync(idUser);
            if (checkMom == null)
            {
                throw new Exception($"Mom profile not found for user {userId}");
            }



            var prNew = new PregnancyTracking
            {
                PregnancyId = newPr.PregnancyId,
                TrackingDate = newPr.TrackingDate,
                WeekNumber = newPr.WeekNumber,
                Weight = newPr.Weight,
                BellySize = newPr.BellySize,
                BloodPressure = newPr.BloodPressure,


            };

            await _context.AddAsync(prNew);
            await _context.SaveChangesAsync();
            return prNew;
        }

        public async Task<IEnumerable<PregnancyTracking>> GetAlLPregnancyTrackingAsync(string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var checkMom = await _profileRepository.GetMomProfileByUserIdAsync(idUser);
            if (checkMom == null)
            {
                throw new Exception($"Mom profile not found for user {userId}");
            }

            var userPre = await _userPregnanciesRepository.GetUserPregnancyByMomIdAsync(checkMom.MomId);
            if (userPre == null)
            {
                throw new Exception($"Pregnancies not found for user {checkMom.MomId}");
            }
            var query = await _context.PregnancyTrackings.Include(x => x.Pregnancy).Where(c => c.PregnancyId == userPre.PregnancyId).ToListAsync();

            return query;
        }
        public async Task<IEnumerable<PregnancyTracking>> GetPregnancyTrackingByIdAsync(int id)
        {
            var check = await _context.PregnancyTrackings.Include(x => x.Pregnancy).Where(x => x.PregnancyId == id).ToListAsync();

            if (check == null)
            {
                throw new Exception($"Pregnancy Tracking {id} is not exist !");
            }

            return check;
        }

        public async Task<PregnancyTracking> GetPregnancyTrackingByUserId(int id)
        {
            var checkMom = await _profileRepository.GetMomProfileByUserIdAsync(id);
            if (checkMom == null)
            {
                throw new Exception($"Mom profile not found for user {id}");
            }

            var userPre = await _userPregnanciesRepository.GetUserPregnancyByMomIdAsync(checkMom.MomId);
            if (userPre == null)
            {
                throw new Exception($"Pregnancies not found for user {checkMom.MomId}");
            }
            var latestTracking = await _context.PregnancyTrackings
                                                                    .Include(x => x.Pregnancy)
                                                                    .Where(c => c.PregnancyId == userPre.PregnancyId)
                                                                    .OrderByDescending(c => c.TrackingDate)
                                                                    .FirstOrDefaultAsync();

            return latestTracking;

        }

        public async Task<PregnancyTracking> UpdatePregnancyTrackingAsync(int id, PregnancyTracking updatePr)
        {
            var check = await _context.PregnancyTrackings.Include(x => x.Pregnancy).FirstOrDefaultAsync(x => x.TrackingId == id);

            if (check == null)
            {
                throw new Exception($"Pregnancy Tracking {id} is not exist !");
            }

            check.TrackingDate = updatePr.TrackingDate;
            check.WeekNumber = updatePr.WeekNumber;
            check.Weight = updatePr.Weight;
            check.BellySize = updatePr.BellySize;
            check.BloodPressure = updatePr.BloodPressure;

            _context.Entry(check).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return check;
        }
    }
}
