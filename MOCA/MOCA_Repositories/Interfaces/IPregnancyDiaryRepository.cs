using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IPregnancyDiaryRepository
    {

        Task<IEnumerable<PregnancyDiary>> GetAlLPregnancyDiary(string userId);
        Task<PregnancyDiary> CreatePregnancyDiary(PregnancyDiary newPD, String userId);
        Task<PregnancyDiary> UpdateMomProfileAsync(int id, PregnancyDiary updatenewPD);
        Task<PregnancyDiary> GetPregnancyDiaryById(int id);
        Task<PregnancyDiary> DeletePregnancyDiaryById(int id);


    }
}
