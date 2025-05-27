 using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.Filter;
using MOCA_Repositories.Models.LessonDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface ILessonsRepository
    {
        Task<Lesson> GetLessonByIdAsync(int id, ClaimsPrincipal userClaims);
        Task<List<LessonViewModel>> ViewActiveLessonsAsync(
            int userId,
            List<FilterCriteria> filters,
            string? sortBy,
            bool isAscending,
            int pageNumber,
            int pageSize,
            int? filterDuration);
        Task<List<Lesson>> AddLessonAsync(AddLessonModel addLessonModel, int userId);
        Task UpdateLessonAsync(int id, UpdateLessonModel updateLessonModel);
        Task<Lesson> DeleteLessonAsync(int id);
    }
}
