using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class BabyTrackingRepository : GenericRepository<BabyTracking>, IBabyTrackingRepository
    {

        private readonly MOCAContext _context;
        private readonly IMomProfileRepository _profileRepository;
        private readonly IUserPregnanciesRepository _userPregnanciesRepository;

   
        public BabyTrackingRepository(MOCAContext context, IMomProfileRepository profileRepository, IUserPregnanciesRepository userPregnanciesRepository)
        {
            _context = context;
            _profileRepository = profileRepository;
            _userPregnanciesRepository = userPregnanciesRepository;
        }



        public async Task<BabyTracking> CreateBabyTrackingAsync(BabyTracking newBb, string userId)
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

            var bbNew = new BabyTracking
            {
                PregnancyId = userPre.PregnancyId,
                CheckupDate = newBb.CheckupDate,
                FetalHeartRate = newBb.FetalHeartRate,
                EstimatedWeight = newBb.EstimatedWeight,
                AmnioticFluidIndex = newBb.AmnioticFluidIndex,
                PlacentaPosition = newBb.PlacentaPosition,
                DoctorComment = newBb.DoctorComment,
                UltrasoundImage = newBb.UltrasoundImage,
                CreatedAt = DateTime.UtcNow
            };

            await _context.AddRangeAsync(bbNew);
            await _context.SaveChangesAsync();
                
            return bbNew;
        }

        public async Task<IEnumerable<BabyTracking>> GetAlLBabyTrackingAsync()
        {
            var query = await _context.BabyTrackings.Include(x => x.Pregnancy).ToListAsync();

            return query;
        }

        public async Task<BabyTracking> GetBabyTrackingByIdAsync(string id)
        {
            if (!int.TryParse(id, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }

            var checkMom = await _profileRepository.GetMomProfileByUserIdAsync(idUser);
            if (checkMom == null)
            {
                throw new Exception($"Mom profile not found for user {idUser}");
            }

            var userPre = await _userPregnanciesRepository.GetUserPregnancyByMomIdAsync(checkMom.MomId);
            var check = await _context.BabyTrackings.Include(x => x.Pregnancy).FirstOrDefaultAsync(x => x.PregnancyId == userPre.PregnancyId);
            if (check == null)
            {
                throw new Exception($"Baby tracking {id} is not exist!");
            }

            return check;
        }

        public async  Task<BabyTracking> UpdateBabyTrackingAsync(int id, BabyTracking updateBb)
        {
            var check = await _context.BabyTrackings.Include(x => x.Pregnancy).FirstOrDefaultAsync(x => x.CheckupBabyId == id);
            if (check == null)
            {
                throw new Exception($"Baby tracking {id} is not exist!");
            }
            check.CheckupDate = updateBb.CheckupDate;
            check.FetalHeartRate = updateBb.FetalHeartRate;
            check.EstimatedWeight = updateBb.EstimatedWeight;
            check.AmnioticFluidIndex = updateBb.AmnioticFluidIndex;
            check.PlacentaPosition = updateBb.PlacentaPosition;
            check.DoctorComment = updateBb.DoctorComment;
            check.UltrasoundImage = updateBb.UltrasoundImage;

            _context.Entry(check).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return check;
           
        }
    }
}
