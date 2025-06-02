using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseCategoryDTO;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository _courseCategoryRepository;
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
        {
            _courseCategoryRepository = courseCategoryRepository;
        }

        public async Task<CourseCategory> AddAsync(CreateCourseCategoryModel create)
        {
            return await _courseCategoryRepository.AddAsync(create);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _courseCategoryRepository.DeleteAsync(id);
        }

        public async Task<List<CourseCategory>> GetAllAsync()
        {
            return await _courseCategoryRepository.GetAllAsync();
        }

        public async Task<CourseCategory?> GetByIdAsync(int id)
        {
            return await _courseCategoryRepository.GetByIdAsync(id);
        }

        public async Task<CourseCategory> UpdateAsync(int id, UpdateCourseCategoryModel update)
        {
            return await _courseCategoryRepository.UpdateAsync(id, update);
        }
    }
}
