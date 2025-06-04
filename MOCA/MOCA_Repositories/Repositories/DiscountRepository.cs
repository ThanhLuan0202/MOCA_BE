using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.DiscountDTO;

namespace MOCA_Repositories.Repositories
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly MOCAContext _context;
        public DiscountRepository(MOCAContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<Discount> AddAsync(CreateDiscountModel create)
        {
            var exists = await _context.Discounts.AnyAsync(x => x.Code == create.Code);
            if (exists)
                throw new InvalidOperationException("Discount code already exists.");

            if (create.StartDate != null && create.EndDate != null &&
                create.EndDate <= create.StartDate)
            {
                throw new InvalidOperationException("End date must be after start date.");
            }

            var discount = new Discount
            {
                Code = create.Code,
                Description = create.Description,
                DiscountType = create.DiscountType,
                Value = create.Value,
                MaxUsage = create.MaxUsage,
                StartDate = create.StartDate,
                EndDate = create.EndDate,
                IsActive = true
            };

            _context.Discounts.Add(discount);
            await _context.SaveChangesAsync();

            return discount;
        }


        public async Task<Discount> DeleteAsync(int id)
        {
            var discount = await _context.Discounts.FindAsync(id);
            if (discount == null)
                return null;

            if (discount.IsActive == false)
                return discount;

            discount.IsActive = false;

            try
            {
                _context.Discounts.Update(discount);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return null;

                return discount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deactivating discount: {ex.Message}");
                return null;
            }
        }


        public async Task<List<Discount>> GetAllAsync()
        {
            return await _context.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetByIdAsync(int id)
        {
            return await _context.Discounts.FindAsync(id);
        }

        public async Task<Discount> UpdateAsync(int id, UpdateDiscountModel update)
        {
            var existing = await _context.Discounts.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Discount with ID {id} not found.");

            if (existing.StartDate <= DateTime.Now)
            {
                if (update.StartDate != existing.StartDate)
                {
                    throw new InvalidOperationException("Cannot modify start date after the discount has started.");
                }

                if (update.Code != existing.Code)
                {
                    throw new InvalidOperationException("Cannot modify code after the discount has started.");
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(update.Code) && update.Code != existing.Code)
                {
                    var codeExists = await _context.Discounts
                        .AnyAsync(x => x.Code == update.Code && x.DiscountId != id);

                    if (codeExists)
                        throw new InvalidOperationException("Another discount with the same code already exists.");
                }

                existing.Code = update.Code ?? existing.Code;
                existing.StartDate = update.StartDate ?? existing.StartDate;
            }

            if (update.StartDate != null && update.EndDate != null &&
                update.EndDate <= update.StartDate)
            {
                throw new InvalidOperationException("End date must be after start date.");
            }

            existing.Description = update.Description ?? existing.Description;
            existing.DiscountType = update.DiscountType ?? existing.DiscountType;
            existing.Value = update.Value ?? existing.Value;
            existing.MaxUsage = update.MaxUsage ?? existing.MaxUsage;
            existing.EndDate = update.EndDate ?? existing.EndDate;

            _context.Discounts.Update(existing);
            await _context.SaveChangesAsync();

            return existing;
        }


    }
}
