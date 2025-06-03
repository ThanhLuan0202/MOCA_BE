using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.PostLikeDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;
        public PostLikeService(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }
        public async Task<PostLike> AddAsync(int userId, CreatePostLikeModel create)
        {
            return await _postLikeRepository.AddAsync(userId, create);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _postLikeRepository.DeleteAsync(id);
        }

        public async Task<List<PostLike>> GetAllAsync()
        {
            return await _postLikeRepository.GetAllAsync();
        }

        public async Task<PostLike?> GetByIdAsync(int id)
        {
            return await _postLikeRepository.GetByIdAsync(id);
        }
    }
}
