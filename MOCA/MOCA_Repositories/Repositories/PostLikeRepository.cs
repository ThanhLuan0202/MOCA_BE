using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PostLikeDTO;

namespace MOCA_Repositories.Repositories
{
    public class PostLikeRepository : Repository<PostLike>, IPostLikeRepository
    {
        private readonly MOCAContext _context;
        public PostLikeRepository(MOCAContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<PostLike> AddAsync(int userId, CreatePostLikeModel create)
        {
            if (!create.PostId.HasValue)
            {
                throw new ArgumentException("PostId is required to create a PostLike.");
            }

            var postId = create.PostId.Value;

            var communityPostExists = await _context.CommunityPosts
                .AnyAsync(c => c.PostId == postId);

            if (!communityPostExists)
            {
                throw new ArgumentException($"CommunityPost with ID {postId} does not exist.");
            }

            var alreadyLiked = await _context.PostLikes
                .AnyAsync(p => p.PostId == postId && p.UserId == userId);

            if (alreadyLiked)
            {
                throw new InvalidOperationException("User has already liked this post.");
            }

            var newPostLike = new PostLike
            {
                PostId = postId,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _context.PostLikes.AddAsync(newPostLike);
            await _context.SaveChangesAsync();

            return newPostLike;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var postLike = await _context.PostLikes.FindAsync(id);

            if (postLike == null)
            {
                return false;
            }

            _context.PostLikes.Remove(postLike);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PostLike>> GetAllAsync()
        {
            return await _context.PostLikes.ToListAsync();
        }

        public async Task<PostLike?> GetByIdAsync(int id)
        {
            return await _context.PostLikes.FindAsync(id);
        }
    }
}
