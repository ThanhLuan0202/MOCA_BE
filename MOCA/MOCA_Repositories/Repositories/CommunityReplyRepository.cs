using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CommunityReplyDTO;

namespace MOCA_Repositories.Repositories
{
    public class CommunityReplyRepository : Repository<CommunityReply>, ICommunityReplyRepository
    {
        private readonly MOCAContext _context;
        public CommunityReplyRepository(MOCAContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<CommunityReply> AddAsync(int userId, CreateCommunityReplyModel create)
        {
            if (create == null)
                throw new ArgumentNullException(nameof(create));

            var post = await _context.CommunityPosts
                .FirstOrDefaultAsync(p => p.PostId == create.PostId);

            if (post == null)
                throw new KeyNotFoundException($"CommunityPost with ID {create.PostId} not found.");

            if (post.Status != "Active")
                throw new InvalidOperationException("You cannot reply to an inactive post.");

            if (create.ParentReplyId.HasValue)
            {
                var parent = await _context.CommunityReplies
                    .FirstOrDefaultAsync(r => r.ReplyId == create.ParentReplyId.Value);

                if (parent == null || parent.PostId != create.PostId)
                    throw new InvalidOperationException("Parent reply does not exist or does not belong to the same post.");
            }

            var newReply = new CommunityReply
            {
                PostId = create.PostId,
                UserId = userId,
                Content = create.Content,
                ParentReplyId = create.ParentReplyId,
                CreatedDate = DateTime.UtcNow
            };

            _context.CommunityReplies.Add(newReply);
            await _context.SaveChangesAsync();

            return newReply;
        }

        public async Task<CommunityReply> DeleteAsync(int id)
        {
            var reply = await _context.CommunityReplies.FindAsync(id);
            if (reply == null || reply.Status == "Inactive")
                return null;

            reply.Status = "Inactive";
            _context.CommunityReplies.Update(reply);
            await _context.SaveChangesAsync();

            return reply;
        }


        public async Task<List<CommunityReply>> GetAllAsync()
        {
            return await _context.CommunityReplies.ToListAsync();
        }

        public async Task<CommunityReply?> GetByIdAsync(int id)
        {
            return await _context.CommunityReplies.FindAsync(id);
        }

        public async Task<CommunityReply> UpdateAsync(int id, UpdateCommunityReplyModel update)
        {
            if (update == null)
                throw new ArgumentNullException(nameof(update));

            var existingReply = await _context.CommunityReplies.FindAsync(id);
            if (existingReply == null)
                throw new KeyNotFoundException($"CommunityReply with ID {id} not found.");

            existingReply.Content = update.Content;

            _context.CommunityReplies.Update(existingReply);
            await _context.SaveChangesAsync();

            return existingReply;
        }

    }
}
