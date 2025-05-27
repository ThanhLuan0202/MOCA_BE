using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.ChapterDTO;

namespace MOCA_Repositories.Interfaces
{
    public interface IChapterRepository
    {
        Task<Chapter?> GetChapterByIdAsync(int id);
        Task<List<ChapterViewModel>> ViewActiveChaptersAsync(
                    int userId,
                    string? searchContent = "",
                    string? sortBy = "ChapterTitle",
                    bool ascending = true,
                    int? pageNumber = 1,
                    int? pageSize = 10
        );
        Task<List<AddChapterModel>> AddChaptersAsync(List<AddChapterModel> addChapterModels, int userId);
        Task UpdateChapter(Chapter chapter);
        Task UpdateChapterAsync(int id, UpdateChapterModel updateChapterModel, int userId);
        Task<Chapter> DeleteChapterAsync(int id);
    }
}
