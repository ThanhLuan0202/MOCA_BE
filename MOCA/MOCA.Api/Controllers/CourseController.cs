using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories;
using MOCA_Repositories.Models.CourseDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchasedCourseServices _purchasedCourseServices;

        public CourseController(ICourseServices courseServices, IUnitOfWork unitOfWork, IPurchasedCourseServices puchasedCourseServices)
        {
            _courseServices = courseServices;
            _unitOfWork = unitOfWork;
            _purchasedCourseServices = puchasedCourseServices;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseViewGET>> GetAllAsync()
        {
            return await _courseServices.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseServices.GetByIdAsync(id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCourseModel updateCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();
            try
            {
                var existingCourse = await _courseServices.GetByIdAsync(id);
                if (existingCourse == null)
                {
                    _unitOfWork.RollbackTransaction();
                    return NotFound($"Course with ID {id} not found.");
                }

                var updatedCourse = await _courseServices.UpdateAsync(id, updateCourse);

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                return Ok(new
                {
                    course = new
                    {
                        updatedCourse.CourseId,
                        updatedCourse.CourseTitle,
                        updatedCourse.Description,
                        updatedCourse.Status,
                        updatedCourse.Price,
                        updatedCourse.Image,
                        updatedCourse.CategoryId,
                        updatedCourse.UserId,
                        updatedCourse.CreateDate
                    }
                });
            }
            catch (KeyNotFoundException ex)
            {
                _unitOfWork.RollbackTransaction();
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var existingCourse = await _courseServices.GetByIdAsync(id);
                if (existingCourse == null)
                {
                    _unitOfWork.RollbackTransaction();
                    return NotFound(new { message = $"Course with ID {id} not found." });
                }

                await _courseServices.DeleteAsync(id);

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                return Ok(new { message = $"Course with ID {id} has been deleted (status set to InActive)." });
            }
            catch (KeyNotFoundException ex)
            {
                _unitOfWork.RollbackTransaction();
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromForm] CreateCourseModel createCourseModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("user is not logged in.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated.");


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdInt = int.Parse(userId);

                var createdCourse = await _courseServices.AddCourseAsync(createCourseModel, userIdInt);
                var courseView = new CourseViewGET
                {
                    CourseId = createdCourse.CourseId,
                    UserId = createdCourse.UserId,
                    CategoryId = createdCourse.CategoryId,
                    CourseTitle = createdCourse.CourseTitle,
                    Description = createdCourse.Description,
                    CreateDate = createdCourse.CreateDate,
                    Status = createdCourse.Status,
                    Image = createdCourse.Image,
                    Price = createdCourse.Price
                };

                return Ok(courseView);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("view-enrolled")]
        public async Task<IActionResult> GetEnrolledCourses(
    string sortByCourseTitle = null,
    bool isAscending = true,
    string filterByInstructor = null,
    string searchByCourseTitle = null,
    int pageNumber = 1,
    int pageSize = 10)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User is not logged in.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not authenticated.");

            var userIdInt = int.Parse(userId);

            var courseIds = await _purchasedCourseServices.GetEnrolledCourseIdsByUserNameAsync(userIdInt);
            if (courseIds == null || !courseIds.Any())
                return NotFound("User has no enrolled courses.");

            var enrolledCourses = await _courseServices.GetByIdAsync(courseIds);
            if (enrolledCourses == null || !enrolledCourses.Any())
                return NotFound("No courses found for the enrolled IDs.");

            // Search by course title
            if (!string.IsNullOrEmpty(searchByCourseTitle))
            {
                enrolledCourses = enrolledCourses
                    .Where(c => c.CourseTitle.Contains(searchByCourseTitle, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filter by instructor name
            if (!string.IsNullOrEmpty(filterByInstructor))
            {
                enrolledCourses = enrolledCourses
                    .Where(c => c.User.FullName == filterByInstructor)
                    .ToList();
            }

            // Sort by CourseTitle if requested
            if (!string.IsNullOrEmpty(sortByCourseTitle))
            {
                enrolledCourses = isAscending
                    ? enrolledCourses.OrderBy(c => c.CourseTitle).ToList()
                    : enrolledCourses.OrderByDescending(c => c.CourseTitle).ToList();
            }

            // Pagination
            var totalRecords = enrolledCourses.Count;
            var pagedCourses = enrolledCourses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map to view model
            var viewCoursesModel = pagedCourses.Select(c => new CourseViewGET
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                Description = c.Description,
                Price = c.Price,
                CategoryId = c.CategoryId,
                UserId = c.UserId,
                Status = c.Status,
                Image = c.Image,
                CreateDate = c.CreateDate
            }).ToList();

            var response = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Courses = viewCoursesModel
            };

            return Ok(response);
        }
    }
}
