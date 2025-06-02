using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CommunityPostDTO;
using MOCA_Repositories.Models.CommunityReplyDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface ICommunityReplyRepository
    {
        Task<List<CommunityReply>> GetAllAsync();
        Task<CommunityReply?> GetByIdAsync(int id);
        Task<CommunityReply> AddAsync(int userId, CreateCommunityReplyModel create);
        Task<CommunityReply> UpdateAsync(int id, UpdateCommunityReplyModel update);
        Task<CommunityReply> DeleteAsync(int id);
    }
}
