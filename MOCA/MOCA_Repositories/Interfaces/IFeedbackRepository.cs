using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.FeedbackDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(int id);
        Task<Feedback> AddAsync(int userId, CreateFeedbackModel feedback);
        Task<Feedback> UpdateAsync(int id, UpdateFeedbackModel feedback);
        Task<bool> DeleteAsync(int id);
    }
}
 