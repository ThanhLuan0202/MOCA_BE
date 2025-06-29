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
    public class UserPregnanciesService : IUserPregnanciesService
    {

        private readonly IUserPregnanciesRepository _repo;

        public UserPregnanciesService(IUserPregnanciesRepository repo)
        {
            _repo = repo;
        }
        public Task<UserPregnancy> CreateUserPregnancyAsync(UserPregnancy newMomPr, string momId)
        {
            return _repo.CreateUserPregnancyAsync(newMomPr, momId);
        }

        public Task<IEnumerable<UserPregnancy>> GetAlLUserPregnancyAsync(string userId)
        {
            return _repo.GetAlLUserPregnancyAsync(userId);
        }

        public Task<UserPregnancy> GetUserPregnancyByIdAsync(int id)
        {
            return _repo.GetUserPregnancyByIdAsync(id);
        }

        public Task<UserPregnancy> GetUserPregnancyByUserId(int id)
        {
            return _repo.GetUserPregnancyByUserId(id);
        }

        public Task<UserPregnancy> UpdateUserPregnancyAsync(int id, UserPregnancy updateMomPr)
        {
            return _repo.UpdateUserPregnancyAsync(id, updateMomPr);
        }
    }
}
