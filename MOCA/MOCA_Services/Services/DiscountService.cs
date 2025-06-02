using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.DiscountDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task<Discount> AddAsync(CreateDiscountModel create)
        {
            return await _discountRepository.AddAsync(create);
        }

        public async Task<Discount> DeleteAsync(int id)
        {
            return await _discountRepository.DeleteAsync(id);
        }

        public Task<List<Discount>> GetAllAsync()
        {
            return _discountRepository.GetAllAsync();
        }

        public Task<Discount?> GetByIdAsync(int id)
        {
            return _discountRepository.GetByIdAsync(id);
        }

        public Task<Discount> UpdateAsync(int id, UpdateDiscountModel update)
        {
            return _discountRepository.UpdateAsync(id, update);
        }
    }
}
