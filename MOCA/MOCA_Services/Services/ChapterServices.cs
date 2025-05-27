using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.ChapterDTO;
using MOCA_Services.Interfaces;

namespace MOCA_Services.Services
{
    public class ChapterServices : IChapterServices
    {
        private readonly IChapterRepository _chapterRepository;
        public ChapterServices(IChapterRepository chapterRepository)
        {
            _chapterRepository = chapterRepository;
        }

        public async Task<List<AddChapterModel>> AddChaptersAsync(List<AddChapterModel> addChapterModel, int userId)
        {
            await _chapterRepository.AddChaptersAsync(addChapterModel, userId);
            return addChapterModel;
        }

        public async Task<Chapter> DeleteChapterAsync(int id)
        {
            return await _chapterRepository.DeleteChapterAsync(id);
        }

        public async Task<Chapter?> GetChapterByIdAsync(int id)
        {
            return await _chapterRepository.GetChapterByIdAsync(id);
        }

        public Task UpdateChapter(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateChapterAsync(int id, UpdateChapterModel updateChapterModel, int userId)
        {
            await _chapterRepository.UpdateChapterAsync(id, updateChapterModel, userId);
        }

        public async Task<List<ChapterViewModel>> ViewActiveChaptersAsync(int userId, string? searchContent, string? sortBy, bool ascending, int? pageNumber, int? pageSize)
        {
            return await _chapterRepository.ViewActiveChaptersAsync(userId, searchContent, sortBy, ascending, pageNumber, pageSize);
        }
    }
}
