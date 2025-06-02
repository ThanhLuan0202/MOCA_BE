using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CommunityReplyDTO;

namespace MOCA_Services.Interfaces
{
    public interface ICommunityReplyService
    {
        Task<List<CommunityReply>> GetAllAsync();
        Task<CommunityReply?> GetByIdAsync(int id);
        Task<CommunityReply> AddAsync(int userId, CreateCommunityReplyModel create);
        Task<CommunityReply> UpdateAsync(int id, UpdateCommunityReplyModel update);
        Task<CommunityReply> DeleteAsync(int id);
    }
}
