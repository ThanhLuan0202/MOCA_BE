using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseDTO;
using MOCA_Repositories.Repositories;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class CourseServices : ICourseServices
    {
        private readonly ICourseRepository _courseRepository;
        public CourseServices(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<Course> AddCourseAsync(CreateCourseModel createCourseModel, int userId)
        {
            return await _courseRepository.AddCourseAsync(createCourseModel, userId);
        }

        public async Task<Course> DeleteAsync(int id)
        {
            return await _courseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseViewGET>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<CourseViewGET>> GetAllCourseActiveAsync(CourseSearchOptions searchOptions)
        {
            return await _courseRepository.GetAllCourseActiveAsync(searchOptions);
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }

        public async Task<List<Course>> GetByIdAsync(List<int> ids)
        {
            return await _courseRepository.GetByIdAsync(ids);
        }

        public async Task<Course> UpdateAsync(int id, UpdateCourseModel course)
        {
            return await _courseRepository.UpdateAsync(id, course);
        }
    }
}
