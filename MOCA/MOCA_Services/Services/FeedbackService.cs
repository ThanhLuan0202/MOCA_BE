using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<Feedback> AddAsync(int userId, CreateFeedbackModel feedback)
        {
            return await _feedbackRepository.AddAsync(userId, feedback);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _feedbackRepository.DeleteAsync(id);
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _feedbackRepository.GetAllAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _feedbackRepository.GetByIdAsync(id);
        }

        public async Task<Feedback> UpdateAsync(int id, UpdateFeedbackModel feedback)
        {
            return await _feedbackRepository.UpdateAsync(id, feedback);
        }
    }
}
