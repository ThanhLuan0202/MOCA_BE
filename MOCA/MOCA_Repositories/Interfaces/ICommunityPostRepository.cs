using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CommunityPostDTO;
using MOCA_Repositories.Models.PurchasedCourseDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface ICommunityPostRepository
    {
        Task<List<CommunityPost>> GetAllAsync();
        Task<CommunityPost?> GetByIdAsync(int id);
        Task<CommunityPost> AddAsync(int userId, CreateCommunityPostModel create);
        Task<CommunityPost> UpdateAsync(int id, UpdateCommunityPostModel update);
        Task<CommunityPost> DeleteAsync(int id);
    }
}
