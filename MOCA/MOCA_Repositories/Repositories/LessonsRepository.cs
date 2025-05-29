using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Generic;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.Filter;
using MOCA_Repositories.Models.LessonDTO;

namespace MOCA_Repositories.Repositories
{
    public class LessonsRepository : Repository<Lesson>, ILessonsRepository
    {
        private readonly MOCAContext _context;
        private readonly IMapper _mapper;
        public LessonsRepository(MOCAContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Lesson>> AddLessonAsync(AddLessonModel addLessonModel, int userId)
        {
            var lessons = new List<Lesson>();

            var chapter = await _context.Chapters.Include(x => x.Course)
                .FirstOrDefaultAsync(c => c.ChapterId == addLessonModel.ChapterId);

            if (chapter == null)
            {
                throw new KeyNotFoundException($"Chapter with ID {addLessonModel.ChapterId} does not exist.");
            }

            if (chapter.Course.UserId != userId)
            {
                throw new InvalidOperationException("User cannot add lesson to a chapter that does not belong to them.");
            }

            if (await Entities.AnyAsync(c => c.ChapterId == addLessonModel.ChapterId &&
                                               c.Title == addLessonModel.Title))
            {
                throw new InvalidOperationException($"Lesson title '{addLessonModel.Title}' must be unique within the chapter.");
            }

            var lesson = new Lesson
            {
                ChapterId = addLessonModel.ChapterId,
                Title = addLessonModel.Title,
                Content = addLessonModel.Content,
                VideoUrl = addLessonModel.VideoURL,
                Duration = addLessonModel.Duration,
                OrderIndex = addLessonModel.OrderIndex,
                CreateDate = DateTime.UtcNow,
                Status = "Active"
            };

            //if (addLessonModel.VideoURL != null)
            //{

            //    var lessonVideo = await UploadFileAsync(addLessonModel.VideoURL);
            //    lesson.VideoURL = lessonVideo;
            //}

            Entities.Add(lesson);
            lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return lessons;
        }

        public async Task<Lesson> DeleteLessonAsync(int id)
        {
            var lesson = await Entities
                .Include(c => c.Chapter)
                .FirstOrDefaultAsync(c => c.LessonId == id);

            if (lesson == null)
            {
                throw new KeyNotFoundException($"Lesson {id} does not exist.");
            }

            lesson.Status = "Inactive";
            _context.Entry(lesson).Property(x => x.Status).IsModified = true;
            await _context.SaveChangesAsync();
            return lesson;
        }

        public async Task<Lesson> GetLessonByIdAsync(int id, ClaimsPrincipal userClaims)
        {
            var userId = int.Parse(userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            return await Entities.Include(x => x.Chapter).FirstOrDefaultAsync(x => x.LessonId == id && x.Status.ToLower() == "Active" && x.Chapter.Course.UserId == userId);
        }

        public async Task UpdateLessonAsync(int id, UpdateLessonModel updateLessonModel)
        {
            var lesson = await Entities.Include(x => x.Chapter).FirstOrDefaultAsync(x => x.LessonId == id);


            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson does not exist.");
            }

            if (!string.IsNullOrEmpty(updateLessonModel.Title))
                lesson.Title = updateLessonModel.Title;

            if (!string.IsNullOrEmpty(updateLessonModel.Content))
                lesson.Content = updateLessonModel.Content;

            if (!string.IsNullOrEmpty(updateLessonModel.VideoURL))
                lesson.VideoUrl = updateLessonModel.VideoURL;

            if (updateLessonModel.Duration.HasValue)
                lesson.Duration = updateLessonModel.Duration.Value;

            if (updateLessonModel.OrderIndex.HasValue)
                lesson.OrderIndex = updateLessonModel.OrderIndex.Value;


            if (updateLessonModel.ChapterId.HasValue)
            {
                var chapterExists = await _context.Chapters.AnyAsync(c => c.ChapterId == updateLessonModel.ChapterId.Value);
                if (!chapterExists)
                {
                    throw new InvalidOperationException($"Chapter {updateLessonModel.ChapterId} does not exist.");
                }
                lesson.ChapterId = updateLessonModel.ChapterId;
            }

            if (await Entities.AnyAsync(c => c.ChapterId == lesson.ChapterId &&
                                             c.Title == lesson.Title &&
                                             c.LessonId != lesson.LessonId))
            {
                throw new InvalidOperationException("Lesson title must be unique within the chapter.");
            }
            //if (updateLessonModel.VideoURL != null)
            //{
            //    var lessonVideo = await UploadFileAsync(updateLessonModel.VideoURL);
            //    lesson.VideoURL = lessonVideo;
            //}
            Entities.Update(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LessonViewModel>> ViewActiveLessonsAsync(
            int userId,
            List<FilterCriteria> filters,
            string? sortBy,
            bool isAscending,
            int pageNumber,
            int pageSize,
            int? filterDuration)
        {
            var lessonsQuery = Entities
                .Include(x => x.Chapter)
                .Where(c => c.Chapter.Course.UserId == userId && c.Status == "Active")
                .AsQueryable();

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    if (!string.IsNullOrEmpty(filter.FilterOn) && !string.IsNullOrEmpty(filter.FilterQuery))
                    {
                        if (filter.FilterOn.Equals("LessonTitle", StringComparison.OrdinalIgnoreCase))
                        {
                            lessonsQuery = lessonsQuery.Where(x => x.Title.Contains(filter.FilterQuery));
                        }
                        //else if (filter.FilterOn.Equals("ChapterTitle", StringComparison.OrdinalIgnoreCase))
                        //{
                        //    lessonsQuery = lessonsQuery.Where(x => x.Chapter.ChapterTitle.Contains(filter.FilterQuery));
                        //}

                    }
                }
            }

            if (filterDuration.HasValue)
            {
                lessonsQuery = lessonsQuery.Where(x => x.Duration == filterDuration.Value);
            }

            if (sortBy != null)
            {
                if (sortBy.Equals("LessonTitle", StringComparison.OrdinalIgnoreCase))
                {
                    lessonsQuery = isAscending ? lessonsQuery.OrderBy(x => x.Title) : lessonsQuery.OrderByDescending(x => x.Title);
                }
                else if (sortBy.Equals("ChapterTitle", StringComparison.OrdinalIgnoreCase))
                {
                    lessonsQuery = isAscending ? lessonsQuery.OrderBy(x => x.Chapter.Title) : lessonsQuery.OrderByDescending(x => x.Chapter.Title);
                }
            }

            var skip = (pageNumber - 1) * pageSize;
            var lessons = await lessonsQuery
                .Skip(skip)
                .Take(pageSize)
                .Select(x => new LessonViewModel
                {
                    LessonId = x.LessonId,
                    Title = x.Title,
                    Content = x.Content,
                    VideoURL = x.VideoUrl,
                    Duration = x.Duration,
                    OrderIndex = x.OrderIndex,
                    ChapterId = x.ChapterId,
                    Status = x.Status
                })
                .ToListAsync();
            return lessons;
        }
    }
}
