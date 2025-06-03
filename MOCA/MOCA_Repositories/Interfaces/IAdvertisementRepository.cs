using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.AdvertisementDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<List<Advertisement>> GetActiveAdsAsync();
        Task<List<Advertisement>> GetAllAsync();
        Task<Advertisement?> GetByIdAsync(int id);
        Task<Advertisement> AddAsync(CreateAdvertisementModel create);
        Task<Advertisement> UpdateAsync(int id, UpdateAdvertisementModel update);
        Task<Advertisement> SetVisibilityAsync(int id, bool visible);
    }
}
