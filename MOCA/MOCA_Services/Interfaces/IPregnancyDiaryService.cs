using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IPregnancyDiaryService
    {
        Task<IEnumerable<PregnancyDiary>> GetAlLPregnancyDiary(string userId);
        Task<PregnancyDiary> CreatePregnancyDiary(PregnancyDiary newPD, String userId);
        Task<PregnancyDiary> UpdateMomProfileAsync(int id, PregnancyDiary updatenewPD);
        Task<PregnancyDiary> GetPregnancyDiaryById(int id);
        Task<PregnancyDiary> DeletePregnancyDiaryById(int id);
    }
}
