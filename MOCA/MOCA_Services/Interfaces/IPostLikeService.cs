using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PostLikeDTO;

namespace MOCA_Services.Interfaces
{
    public interface IPostLikeService
    {
        Task<List<PostLike>> GetAllAsync();
        Task<PostLike?> GetByIdAsync(int id);
        Task<PostLike> AddAsync(int userId, CreatePostLikeModel create);
        Task<bool> DeleteAsync(int id);
    }
}
