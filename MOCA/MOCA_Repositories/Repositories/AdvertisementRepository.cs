using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.AdvertisementDTO;

namespace MOCA_Repositories.Repositories
{
    public class AdvertisementRepository : Repository<Advertisement>, IAdvertisementRepository
    {
        private readonly MOCAContext _context;
        public AdvertisementRepository(MOCAContext context) :base(context) 
        {
            _context = context;
        }
        public async Task<Advertisement> AddAsync(CreateAdvertisementModel create)
        {
            var now = DateTime.UtcNow;

            if (create.StartDate != null && create.StartDate < now)
            {
                throw new InvalidOperationException("Start date must be greater than or equal to the current time.");
            }

            if (create.StartDate != null && create.EndDate != null &&
               create.EndDate <= create.StartDate)
            {
                throw new InvalidOperationException("End date must be after start date.");
            }

            var newAdvertisement = new Advertisement
            {
                Title = create.Title,
                ImageUrl = create.ImageUrl,
                RedirectUrl = create.RedirectUrl,
                StartDate = create.StartDate,
                EndDate = create.EndDate,
                CreateDate = now,
                IsVisible = true,
            };

            _context.Advertisements.Add(newAdvertisement);
            await _context.SaveChangesAsync();
            return newAdvertisement;
        }

        public async Task<List<Advertisement>> GetActiveAdsAsync()
        {
            var now = DateTime.UtcNow;

            return await _context.Advertisements
                .Where(ad => ad.IsVisible == true
                             && ad.StartDate <= now
                             && ad.EndDate >= now)
                .ToListAsync();
        }

        public async Task<List<Advertisement>> GetAllAsync()
        {
            return await _context.Advertisements.ToListAsync();
        }

        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _context.Advertisements.FindAsync(id);
        }

        public async Task<Advertisement> SetVisibilityAsync(int id, bool visible)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            if (ad == null)
                throw new KeyNotFoundException($"Advertisement with ID {id} not found.");

            ad.IsVisible = visible;
            _context.Advertisements.Update(ad);
            await _context.SaveChangesAsync();
            return ad;
        }

        public async Task<Advertisement> UpdateAsync(int id, UpdateAdvertisementModel update)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            if (ad == null)
            {
                throw new KeyNotFoundException($"Advertisement with ID {id} not found.");
            }

            var now = DateTime.UtcNow;

            if (update.StartDate != null && update.StartDate < now)
            {
                throw new InvalidOperationException("Start date must be greater than or equal to the current time.");
            }

            if (update.StartDate != null && update.EndDate != null &&
                update.EndDate <= update.StartDate)
            {
                throw new InvalidOperationException("End date must be after start date.");
            }

            ad.Title = update.Title ?? ad.Title;
            ad.ImageUrl = update.ImageUrl ?? ad.ImageUrl;
            ad.RedirectUrl = update.RedirectUrl ?? ad.RedirectUrl;
            ad.StartDate = update.StartDate ?? ad.StartDate;
            ad.EndDate = update.EndDate ?? ad.EndDate;
            ad.IsVisible = true;

            _context.Advertisements.Update(ad);
            await _context.SaveChangesAsync();
            return ad;
        }

    }
}
