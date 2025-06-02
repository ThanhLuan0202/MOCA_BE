using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CommunityPostDTO;

namespace MOCA_Repositories.Repositories
{
    public class CommunityPostRepository : Repository<CommunityPost>, ICommunityPostRepository
    {
        private readonly MOCAContext _context;
        public CommunityPostRepository(MOCAContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<CommunityPost> AddAsync(int userId, CreateCommunityPostModel create)
        {
            
            var newCommunityPost = new CommunityPost
            {
                UserId = userId,
                Title = create.Title,
                Content = create.Content,
                Tags = create.Tags,
                ImageUrl = create.ImageUrl,
                Status = "Active",
                CreateDate = DateTime.UtcNow
            };

            _context.CommunityPosts.Add(newCommunityPost);
            await _context.SaveChangesAsync();
            return newCommunityPost;
        }

        public async Task<CommunityPost> DeleteAsync(int id)
        {
            var communityPost = await _context.CommunityPosts.FindAsync(id);
            if (communityPost == null)
                return null;

            if (communityPost.Status == "Inactive")
                return communityPost;

            communityPost.Status = "Inactive";

            try
            {
                _context.CommunityPosts.Update(communityPost);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return null;

                return communityPost;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deactivating communityPost: {ex.Message}");
                return null;
            }
        }

        public async Task<List<CommunityPost>> GetAllAsync()
        {
            return await _context.CommunityPosts.ToListAsync();
        }

        public async Task<CommunityPost?> GetByIdAsync(int id)
        {
            return await _context.CommunityPosts.FindAsync(id);
        }

        public async Task<CommunityPost> UpdateAsync(int id, UpdateCommunityPostModel update)
        {
            var existing = await _context.CommunityPosts.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"CommunityPost with id {id} not found");

            existing.Title = update.Title;
            existing.Content = update.Content;
            existing.Tags = update.Tags;
            existing.ImageUrl = update.ImageUrl;
            existing.Status = "Active";
            _context.CommunityPosts.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }
    }
}
