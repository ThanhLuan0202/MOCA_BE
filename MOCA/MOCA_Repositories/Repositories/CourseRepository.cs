using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOCA_Repositories.DBContext;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Repositories.Models.CourseDTO;

namespace MOCA_Repositories.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly MOCAContext _context;
        private readonly IMapper _map;
        public CourseRepository(MOCAContext context, IMapper mapper) : base(context) 
        {
            _context = context;
            _map = mapper;
        }

        public async Task<Course> AddCourseAsync(CreateCourseModel createCourseModel, int userId)
        {
            if (createCourseModel == null)
            {
                throw new ArgumentNullException(nameof(createCourseModel));
            }

            var categoryExists = await _context.CourseCategories.AnyAsync(c => c.CategoryId == createCourseModel.CategoryId /*&& c.Status.ToLower() == "active"*/);

            if (!categoryExists)
            {
                throw new InvalidOperationException("Category does not exist.");
            }

            var existingCourse = await Entities.FirstOrDefaultAsync(c => c.CourseTitle == createCourseModel.CourseTitle);

            if (existingCourse != null)
            {
                throw new InvalidOperationException("Course title must be unique.");
            }


            var course = new Course
            {
                CategoryId = createCourseModel.CategoryId,
                CourseTitle = createCourseModel.CourseTitle,
                Description = createCourseModel.Description,
                Status = createCourseModel.Status,
                Price = createCourseModel.Price,
                UserId = userId,
                CreateDate = createCourseModel.CreateDate ?? DateTime.UtcNow.AddHours(7),
            };

            if (createCourseModel.Image != null)
            {
                //var imageUrl = await UploadFileAsync(createCourseModel.Image);
                course.Image = createCourseModel.Image;
            }

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<Course> DeleteAsync(int id)
        {
            var course = await GetByIdAsync(id);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with Id {id} not found.");
            }

            course.Status = "InActive";
            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<IEnumerable<CourseViewGET>> GetAllAsync()
        {
            var query = Entities.Include(c => c.Category)
                                .Include(c => c.User)
                                .Include(c => c.PurchasedCourses)
                                     //.ThenInclude(pc => pc.Feedbacks)
                                .Where(c => c.Status.ToLower() == "active");
            return (IEnumerable<CourseViewGET>)query;
        }

        public async Task<IEnumerable<CourseViewGET>> GetAllCourseActiveAsync(CourseSearchOptions searchOptions)
        {
            var query = Entities.Include(c => c.Category)
                                .Include(c => c.User)
                                .Include(c => c.Chapters)
                                .Include(c => c.PurchasedCourses)
                                     //.ThenInclude(pc => pc.Feedback)
                                .Where(c => c.Status.ToLower() == "active");
            
            if (!string.IsNullOrEmpty(searchOptions.CourseTitle))
            {
                query = query.Where(c => c.CourseTitle.Contains(searchOptions.CourseTitle));
            }

            if (!string.IsNullOrEmpty(searchOptions.CategoryName))
            {
                query = query.Where(c => c.Category.Name.Contains(searchOptions.CategoryName));
            }


            var courses = await query.ToListAsync();

            var courseViews = _map.Map<IEnumerable<CourseViewGET>>(courses);

            return courseViews.OrderByDescending(c => c.CourseTitle);
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(x => x.CourseId == id && x.Status.ToLower() == "active");
        }

        public async Task<List<Course>> GetByIdAsync(List<int> ids)
        {
            return await Entities
                 .Where(x => ids.Contains(x.CourseId) && x.Status.ToLower() == "active")
                 .Include(x => x.User)
                 .ToListAsync();
        }

        //public async Task<int> GetCourseRatingAsync(int courseId)
        //{
        //    var course = await Entities.Include(c => c.PurchasedCourses)
        //                               .ThenInclude(pc => pc.Feedback)
        //                               .FirstOrDefaultAsync(c => c.CourseId == courseId);

        //    if (course == null)
        //    {
        //        return 0;
        //    }

        //    var feedbacks = course.PurchasedCourses.SelectMany(pc => pc.Feedback).ToList();
        //    if (!feedbacks.Any())
        //    {
        //        return 0;
        //    }

        //    int totalStar = feedbacks.Where(f => f.Star.HasValue).Sum(f => f.Star.Value);
        //    int rate = totalStar / feedbacks.Count(f => f.Star.HasValue);

        //    return rate;
        //}

        public async Task<Course> UpdateAsync(int id, UpdateCourseModel course)
        {
            var existingCourse = await GetByIdAsync(id);

            if (existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with Id {id} not found.");
            }


            var courseSimilarValidation = await Entities.FirstOrDefaultAsync(c => c.CourseTitle == course.CourseTitle && c.CourseId != id);

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }

            if (courseSimilarValidation != null)
            {
                throw new InvalidOperationException($"Course with title '{course.CourseTitle}' already exists.");
            }

            var categoryExists = await _context.CourseCategories.AnyAsync(c => c.CategoryId == course.CategoryId /*&& c.Status.ToLower() == "active"*/);
            if (!categoryExists)
            {
                throw new KeyNotFoundException($"Category with Id {course.CategoryId} not found.");
            }

            if (course.Image != null)
            {
                //var imageUrl = await UploadFileAsync(course.Image);
                existingCourse.Image = course.Image;
            }

            existingCourse.CategoryId = course.CategoryId ?? existingCourse.CategoryId;
            existingCourse.CourseTitle = course.CourseTitle ?? existingCourse.CourseTitle;
            existingCourse.Description = course.Description ?? existingCourse.Description;
            existingCourse.Price = course.Price ?? existingCourse.Price;
            //_dbContext.Course.Update(course);
            Entities.Update(existingCourse);
            await _context.SaveChangesAsync();
            return existingCourse;
        }
    }
}
