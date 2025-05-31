using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.FeedbackDTO;

namespace MOCA_Repositories.Repositories
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly MOCAContext _context;

        public FeedbackRepository(MOCAContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }

        public async Task<Feedback> AddAsync(int userId, CreateFeedbackModel feedback)
        {
            if (feedback.CourseId.HasValue)
            {
                var courseExists = await _context.Courses
                    .AnyAsync(c => c.CourseId == feedback.CourseId.Value);

                if (!courseExists)
                {
                    throw new ArgumentException($"Course with ID {feedback.CourseId.Value} does not exist.");
                }
            }
            var newFeedback = new Feedback
            {
                UserId = userId, 
                CourseId = feedback.CourseId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreateDate = DateTime.UtcNow
            };

            _context.Feedbacks.Add(newFeedback);
            await _context.SaveChangesAsync();
            return newFeedback;
        }

        public async Task<Feedback> UpdateAsync(int id, UpdateFeedbackModel feedback)
        {
            var existing = await _context.Feedbacks.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Feedback with id {id} not found");

            existing.Rating = feedback.Rating;
            existing.Comment = feedback.Comment;
            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return false;

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
