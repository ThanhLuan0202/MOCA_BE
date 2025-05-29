using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CourseDTO;

namespace MOCA_Services.Interfaces
{
    public interface ICourseServices
    {
        Task<Course> GetByIdAsync(int id);
        Task<IEnumerable<CourseViewGET>> GetAllAsync();
        Task<Course> GetByIdAllStatusAsync(int id);
        Task<Course> AddCourseAsync(CreateCourseModel createCourseModel, int userId);
        Task<Course> UpdateAsync(int id, UpdateCourseModel course);
        Task<Course> DeleteAsync(int id);
        Task<IEnumerable<CourseViewGET>> GetAllCourseActiveAsync(CourseSearchOptions searchOptions);
        //Task<int> GetCourseRatingAsync(int courseId);
        Task<List<Course>> GetByIdAsync(List<int> ids);
    }
}
