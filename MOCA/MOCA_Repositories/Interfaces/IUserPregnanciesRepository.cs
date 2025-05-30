using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.MomProfileDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Repositories.Interfaces
{
    public interface IUserPregnanciesRepository
    {
        Task<IEnumerable<UserPregnancy>> GetAlLUserPregnancyAsync();
        Task<UserPregnancy> CreateUserPregnancyAsync(UserPregnancy newMomPr, String momId);
        Task<UserPregnancy> UpdateUserPregnancyAsync(int id, UserPregnancy updateMomPr);

        Task<UserPregnancy> GetUserPregnancyByIdAsync(int id);


    }
}
