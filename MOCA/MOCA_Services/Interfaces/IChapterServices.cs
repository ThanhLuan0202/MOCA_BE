using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.ChapterDTO;

namespace MOCA_Services.Interfaces
{
    public interface IChapterServices
    {
        Task<Chapter?> GetChapterByIdAsync(int id);
        Task<List<ChapterViewModel>> ViewActiveChaptersAsync(
                    int userId,
                    string? searchContent,
                    string? sortBy,
                    bool ascending,
                    int? pageNumber,
                    int? pageSize
        );
        Task<List<AddChapterModel>> AddChaptersAsync(List<AddChapterModel> addChapterModel, int userId);
        Task UpdateChapter(Chapter chapter);
        Task UpdateChapterAsync(int id, UpdateChapterModel updateChapterModel, int userId);
        Task<Chapter> DeleteChapterAsync(int id);
    }
}
