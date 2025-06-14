using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Repositories
{
    public class PregnancyDiaryRepository : GenericRepository<PregnancyDiary>, IPregnancyDiaryRepository
    {
        private readonly MOCAContext _context;
        private readonly IMomProfileRepository _mom;

        public PregnancyDiaryRepository(MOCAContext context, IMomProfileRepository mom)
        {
            _context = context;
            _mom = mom;
        }

        public async Task<PregnancyDiary> CreatePregnancyDiary(PregnancyDiary newPD, string userId)
        {
            if (!int.TryParse(userId, out int iduser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var momId = await _mom.GetMomProfileByUserIdAsync(iduser);

            var newPregnancyDiary = new PregnancyDiary
            {
                MomID = momId.MomId,
                Feeling = newPD.Feeling,
                Title = newPD.Title,
                CreateDate = DateTime.Now
            };

            await _context.AddAsync(newPregnancyDiary);
            await _context.SaveChangesAsync();
            return newPregnancyDiary;
        }

        public async Task<PregnancyDiary> DeletePregnancyDiaryById(int id)
        {
            var checkPreg = await _context.PregnancyDiaries
                .Include(x => x.MomProfile)
                .FirstOrDefaultAsync(b => b.PregnancyID == id);
            if (checkPreg == null)
            {
                throw new Exception($"Pregnancy diary with ID {id} not found.");
            }
            _context.PregnancyDiaries.Remove(checkPreg);
            await _context.SaveChangesAsync();
            return checkPreg;
        }

        public async Task<IEnumerable<PregnancyDiary>> GetAlLPregnancyDiary(string userId)
        {
            if (!int.TryParse(userId, out int iduser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var momId = await _mom.GetMomProfileByUserIdAsync(iduser);

            var query = await _context.PregnancyDiaries
                .Include(x => x.MomProfile)
                .Where(c => c.MomID == momId.MomId)
                .ToListAsync();

            return query;
        }

        public async Task<PregnancyDiary> GetPregnancyDiaryById(int id)
        {
            var checkPreg = await _context.PregnancyDiaries
                .Include(x => x.MomProfile)
                .FirstOrDefaultAsync(b => b.PregnancyID == id);
            if (checkPreg == null)
            {
                throw new Exception($"Pregnancy diary with ID {id} not found.");
            }
            return checkPreg;
        }

        public async Task<PregnancyDiary> UpdateMomProfileAsync(int id, PregnancyDiary updatenewPD)
        {
            var checkPreg = await _context.PregnancyDiaries
                .Include(x => x.MomProfile)
                .FirstOrDefaultAsync(b => b.PregnancyID == id);
            if (checkPreg == null)
            {
                throw new Exception($"Pregnancy diary with ID {id} not found.");
            }
            checkPreg.Title = updatenewPD.Title;

            _context.Entry(checkPreg).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return checkPreg;
        }
    }
}
