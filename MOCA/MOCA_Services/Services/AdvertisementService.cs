using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.AdvertisementDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<Advertisement> AddAsync(CreateAdvertisementModel create)
        {
            return await _advertisementRepository.AddAsync(create);
        }

        public async Task<List<Advertisement>> GetActiveAdsAsync()
        {
            return await _advertisementRepository.GetActiveAdsAsync();
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            return await _advertisementRepository.GetAllAsync();
        }

        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _advertisementRepository.GetByIdAsync(id);
        }

        public async Task<Advertisement> SetVisibilityAsync(int id, bool visible)
        {
            return await _advertisementRepository.SetVisibilityAsync(id, visible);
        }

        public async Task<Advertisement> UpdateAsync(int id, UpdateAdvertisementModel update)
        {
            return await _advertisementRepository.UpdateAsync(id, update);
        }
    }
}
