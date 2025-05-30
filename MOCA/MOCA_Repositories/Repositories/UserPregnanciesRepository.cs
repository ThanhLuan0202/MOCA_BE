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
    public class UserPregnanciesRepository : GenericRepository<UserPregnancy>, IUserPregnanciesRepository
    {

        private readonly MOCAContext _context;
        private readonly IMomProfileRepository _momProfileRepository;

        public UserPregnanciesRepository(MOCAContext context, IMomProfileRepository momProfileRepository)
        {
            _context = context;
            _momProfileRepository = momProfileRepository;
        }

        public async Task<UserPregnancy> CreateUserPregnancyAsync(UserPregnancy newMomPr, string userId)
        {
            if (!int.TryParse(userId, out int idUser))
            {
                throw new ArgumentException("Invalid user ID");
            }
            var findMom = await _momProfileRepository.GetMomProfileByUserIdAsync(idUser);

            var idMom = findMom.MomId;

            var newUserPre = new UserPregnancy
            {
                MomId = idMom,
                StartDate = newMomPr.StartDate,
                DueDate = newMomPr.DueDate,
                Notes = newMomPr.Notes,
                CreatedAt = DateTime.UtcNow
            };
            
            await _context.AddAsync(newUserPre);

            await _context.SaveChangesAsync();

            return newUserPre;
        }

        public async Task<IEnumerable<UserPregnancy>> GetAlLUserPregnancyAsync()
        {
            var query = await _context.UserPregnancies.Include(x => x.Mom).ToListAsync();
            return query;
        }

        public async Task<UserPregnancy> GetUserPregnancyByIdAsync(int id)
        {
            var check = await _context.UserPregnancies.Include(x => x.Mom).FirstOrDefaultAsync(x => x.PregnancyId == id);
            if (check == null)
            {
                throw new Exception($"User pregnancy {id} is not exist !");
            }

            return check;

        }

        public async Task<UserPregnancy> UpdateUserPregnancyAsync(int id, UserPregnancy updateMomPr)
        {
            var check = await _context.UserPregnancies.Include(x => x.Mom).FirstOrDefaultAsync(x => x.PregnancyId == id);
            if (check == null)
            {
                throw new Exception($"User pregnancy {id} is not exist !");
            }

            check.StartDate = updateMomPr.StartDate;
            check.DueDate = updateMomPr.DueDate;
            check.Notes = updateMomPr.Notes;

            _context.Entry(check).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return check;
        }
    }
}
