using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.Filter;
using MOCA_Repositories.Models.LessonDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class LessonServices : ILessonServices
    {
        private readonly ILessonsRepository _lessonRepository;

        public LessonServices(ILessonsRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<AddLessonModel> AddLessonAsync(AddLessonModel addLessonModel, int userId)
        {
            await _lessonRepository.AddLessonAsync(addLessonModel, userId);
            return addLessonModel;
        }

        public async Task<Lesson> DeleteLessonAsync(int id)
        {
            return await _lessonRepository.DeleteLessonAsync(id);
        }

        public async Task<Lesson> GetLessonByIdAsync(int id, ClaimsPrincipal userClaims)
        {
            return await _lessonRepository.GetLessonByIdAsync(id, userClaims);
        }

        public async Task UpdateLessonAsync(int id, UpdateLessonModel updateLessonModel)
        {
            await _lessonRepository.UpdateLessonAsync(id, updateLessonModel);
        }

        public async Task<List<LessonViewModel>> ViewActiveLessonsAsync(int userId, List<FilterCriteria> filters, string? sortBy, bool isAscending, int pageNumber, int pageSize, int? filterDuration)
        {
            return await _lessonRepository.ViewActiveLessonsAsync(userId, filters, sortBy, isAscending, pageNumber, pageSize, filterDuration);
        }
    }
}