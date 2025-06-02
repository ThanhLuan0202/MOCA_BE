using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CommunityReplyDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CommunityReplyService : ICommunityReplyService
    {
        private readonly ICommunityReplyRepository _communityReplyRepository;
        public CommunityReplyService(ICommunityReplyRepository communityReplyRepository)
        {
            _communityReplyRepository = communityReplyRepository;
        }

        public async Task<CommunityReply> AddAsync(int userId, CreateCommunityReplyModel create)
        {
            return await _communityReplyRepository.AddAsync(userId, create);
        }

        public async Task<CommunityReply> DeleteAsync(int id)
        {
            return await _communityReplyRepository.DeleteAsync(id);
        }

        public async Task<List<CommunityReply>> GetAllAsync()
        {
            return await _communityReplyRepository.GetAllAsync();
        }

        public async Task<CommunityReply?> GetByIdAsync(int id)
        {
            return await _communityReplyRepository.GetByIdAsync(id);
        }

        public async Task<CommunityReply> UpdateAsync(int id, UpdateCommunityReplyModel update)
        {
            return await _communityReplyRepository.UpdateAsync(id, update);
        }
    }
}
