using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Extensions;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.ChapterDTO;

namespace MOCA_Repositories.Repositories
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        private readonly MOCAContext _context;

        public ChapterRepository(MOCAContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<AddChapterModel>> AddChaptersAsync(List<AddChapterModel> addChapterModels, int userId)
        {
            foreach (var addChapterModel in addChapterModels)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == addChapterModel.CourseId);

                if (course == null)
                {
                    throw new KeyNotFoundException($"Course {addChapterModel.CourseId} does not exist.");
                }

                if (course.UserId != userId)
                {
                    throw new InvalidOperationException($"User cannot add chapters to course {addChapterModel.CourseId} that does not belong to them.");
                }

                if (string.IsNullOrWhiteSpace(addChapterModel.Title))
                {
                    throw new InvalidOperationException($"Chapter title must not be null.");
                }

                if (await _context.Chapters.AnyAsync(c => c.CourseId == addChapterModel.CourseId && c.Title == addChapterModel.Title))
                {
                    throw new InvalidOperationException($"Chapter title '{addChapterModel.Title}' must be unique within the course {addChapterModel.CourseId}.");
                }

                if (ContainsSpecialCharacters(addChapterModel.Title))
                {
                    throw new ArgumentException("ChapterTitle cannot contain special characters.");
                }


                var chapter = new Chapter
                {
                    CourseId = addChapterModel.CourseId,
                    Title = addChapterModel.Title,
                    OrderIndex = addChapterModel.OrderIndex,
                    Status = "Active",
                };

                _context.Chapters.Add(chapter);
            }

            await _context.SaveChangesAsync();

            return addChapterModels;
        }

        public async Task<Chapter> DeleteChapterAsync(int id)
        {
            var chapter = await _context.Chapters
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.ChapterId == id);

            if (chapter == null)
            {
                throw new KeyNotFoundException($"Chapter {id} does not exist.");
            }

            if (chapter.Lessons.Any())
            {
                throw new InvalidOperationException("This Chapter is already had Students studying. Cannot inactive!");
            }

            chapter.Status = "Inactive";
            _context.Entry(chapter).Property(x => x.Status).IsModified = true;
            await _context.SaveChangesAsync();

            return chapter;
        }

        public async Task<Chapter?> GetChapterByIdAsync(int id)
        {
            return await Entities
              .Include(x => x.Course)
              .FirstOrDefaultAsync(x => x.ChapterId == id);
        }

        public async Task UpdateChapter(Chapter chapter)
        {
            _context.Chapters.Update(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChapterAsync(int id, UpdateChapterModel updateChapterModel, int userId)
        {
            var chapter = await _context.Chapters
                .Include(c => c.Course)
                .FirstOrDefaultAsync(c => c.ChapterId == id);

            if (chapter == null)
            {
                throw new KeyNotFoundException("Chapter does not exist.");
            }

            if (chapter.Course == null || chapter.Course.UserId != userId)
            {
                throw new InvalidOperationException("User is not authorized to update this chapter.");
            }

            if (updateChapterModel.CourseId.HasValue && updateChapterModel.CourseId.Value > 0)
            {
                var targetCourse = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == updateChapterModel.CourseId.Value);

                if (targetCourse == null)
                {
                    throw new InvalidOperationException($"Course {updateChapterModel.CourseId.Value} does not exist.");
                }

                if (targetCourse.UserId != userId)
                {
                    throw new InvalidOperationException("User is not authorized to assign the chapter to this course.");
                }

                chapter.CourseId = updateChapterModel.CourseId.Value;
            }

            chapter.Title = updateChapterModel.Title ?? chapter.Title;
            chapter.OrderIndex = updateChapterModel.OrderIndex ?? chapter.OrderIndex;

            if (await _context.Chapters.AnyAsync(c => c.CourseId == chapter.CourseId &&
                                                     c.Title == chapter.Title &&
                                                     c.ChapterId != chapter.ChapterId))
            {
                throw new InvalidOperationException("Chapter title must be unique within the course.");
            }

            await UpdateChapter(chapter);
        }

        public async Task<List<ChapterViewModel>> ViewActiveChaptersAsync(int userId, string? searchContent = "", string? sortBy = "ChapterTitle", bool ascending = true, int? pageNumber = 1, int? pageSize = 10)
        {
            int actualPageNumber = pageNumber ?? 1;
            int actualPageSize = pageSize ?? 10;

            var chaptersQuery = _context.Chapters
                .Include(x => x.Course)
                .Where(c => c.Course.UserId == userId)
                .FilterByStatus("Active")
                .SearchByTitle(searchContent);

            var sortColumn = sortBy?.ToLower() ?? "chaptertitle";

            chaptersQuery = sortColumn switch
            {
                "chaptertitle" => ascending ? chaptersQuery.OrderBy(c => c.Title) : chaptersQuery.OrderByDescending(c => c.Title),
                "coursetitle" => ascending ? chaptersQuery.OrderBy(c => c.Course.CourseTitle) : chaptersQuery.OrderByDescending(c => c.Course.CourseTitle),
                _ => chaptersQuery
            };

            chaptersQuery = chaptersQuery.ApplyPaging(actualPageNumber, actualPageSize);

            var chapters = await chaptersQuery
                .Select(c => new ChapterViewModel
                {
                    ChapterId = c.ChapterId,
                    CourseId = c.CourseId,
                    Title = c.Title,
                    OrderIndex = c.OrderIndex,
                    Status = c.Status,
                })
                .ToListAsync();

            return chapters;
        }

        private bool ContainsSpecialCharacters(string? input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            return input.Any(ch => "!@$%^&*()_+={}[]:;\"'<>,.?/".Contains(ch));
        }
        
    }
}
