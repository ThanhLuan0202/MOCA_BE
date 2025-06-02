using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.DiscountDTO;
using MOCA_Repositories.Models.FeedbackDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllAsync();
        Task<Discount?> GetByIdAsync(int id);
        Task<Discount> AddAsync(CreateDiscountModel create);
        Task<Discount> UpdateAsync(int id, UpdateDiscountModel update);
        Task<Discount> DeleteAsync(int id);
    }
}
