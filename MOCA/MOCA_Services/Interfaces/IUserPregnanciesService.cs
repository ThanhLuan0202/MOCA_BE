using MOCA_Repositories.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA_Services.Interfaces
{
    public interface IUserPregnanciesService
    {
        Task<IEnumerable<UserPregnancy>> GetAlLUserPregnancyAsync(string userId);
        Task<UserPregnancy> CreateUserPregnancyAsync(UserPregnancy newMomPr, String momId);
        Task<UserPregnancy> UpdateUserPregnancyAsync(int id, UserPregnancy updateMomPr);

        Task<UserPregnancy> GetUserPregnancyByIdAsync(int id);
    }
}
