using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CourseCategoryDTO;
using MOCA_Repositories.Models.FeedbackDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface ICourseCategoryRepository
    {
        Task<List<CourseCategory>> GetAllAsync();
        Task<CourseCategory?> GetByIdAsync(int id);
        Task<CourseCategory> AddAsync(CreateCourseCategoryModel create);
        Task<CourseCategory> UpdateAsync(int id, UpdateCourseCategoryModel update);
        Task<bool> DeleteAsync(int id);
    }
}
