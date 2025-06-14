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
    public class PregnancyDiaryService : IPregnancyDiaryService
    {
        private readonly IPregnancyDiaryRepository _repo;

        public PregnancyDiaryService(IPregnancyDiaryRepository repo)
        {
            _repo = repo;
        }
        public Task<PregnancyDiary> CreatePregnancyDiary(PregnancyDiary newPD, string userId)
        {
            return _repo.CreatePregnancyDiary(newPD, userId);
        }

        public Task<PregnancyDiary> DeletePregnancyDiaryById(int id)
        {
            return _repo.DeletePregnancyDiaryById(id);
        }

        public Task<IEnumerable<PregnancyDiary>> GetAlLPregnancyDiary(string userId)
        {
            return _repo.GetAlLPregnancyDiary(userId);
        }

        public Task<PregnancyDiary> GetPregnancyDiaryById(int id)
        {
            return _repo.GetPregnancyDiaryById(id);
        }

        public Task<PregnancyDiary> UpdateMomProfileAsync(int id, PregnancyDiary updatenewPD)
        {
            return _repo.UpdateMomProfileAsync(id, updatenewPD);
        }
    }
}
