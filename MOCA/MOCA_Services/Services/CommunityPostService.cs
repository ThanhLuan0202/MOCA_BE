using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CommunityPostDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository _communityPostRepository;
        public CommunityPostService(ICommunityPostRepository communityPostRepository)
        {
            _communityPostRepository = communityPostRepository;
        }
        public async Task<CommunityPost> AddAsync(int userId, CreateCommunityPostModel create)
        {
            return await _communityPostRepository.AddAsync(userId, create);
        }

        public async Task<CommunityPost> DeleteAsync(int id)
        {
            return await _communityPostRepository.DeleteAsync(id);
        }

        public async Task<List<CommunityPost>> GetAllAsync()
        {
            return await _communityPostRepository.GetAllAsync();
        }

        public async Task<CommunityPost?> GetByIdAsync(int id)
        {
            return await _communityPostRepository.GetByIdAsync(id);
        }

        public async Task<CommunityPost> UpdateAsync(int id, UpdateCommunityPostModel update)
        {
            return await _communityPostRepository.UpdateAsync(id, update);
        }
    }
}
