using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories;
using MOCA_Repositories.Models.Filter;
using MOCA_Repositories.Models.LessonDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    public class LessonController : ControllerBase
    {
        private readonly ILessonServices _lessonServices;
        private readonly IChapterServices _chapterServices;
        private readonly IUnitOfWork _unitOfWork;

        public LessonController(ILessonServices lessonServices, IUnitOfWork unitOfWork, IChapterServices chapterServices)
        {
            _lessonServices = lessonServices;
            _unitOfWork = unitOfWork;
            _chapterServices = chapterServices;
        }

        [HttpPost("add-lesson")]
        public async Task<IActionResult> AddLesson([FromForm] AddLessonModel addLessonModel)
        {

            //var userNameIdentifierClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //if (userNameIdentifierClaim == null)
            //{
            //    return Unauthorized("User is not Logged In!.");
            //}

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User is not Logged In!.");
            }

            try
            {
                _unitOfWork.BeginTransaction();

                var userIdz = int.Parse(userId);

                var addedLesson = await _lessonServices.AddLessonAsync(addLessonModel, userIdz);
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                return Ok(addedLesson);
            }
            catch (KeyNotFoundException ex)
            {
                _unitOfWork.RollbackTransaction();
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(500, ex.InnerException);
            }
        }

        [HttpPut("update-lesson/{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromForm] UpdateLessonModel updateLessonModel)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("User is not logged in.");
                }

                var usernameFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (usernameFromToken == null)
                {
                    return Unauthorized("Token does not contain a valid UserName.");
                }

                var existingLesson = await _lessonServices.GetLessonByIdAsync(id, User);

                if (existingLesson == null)
                {
                    throw new KeyNotFoundException($"Lesson with Id {id} not found.");
                }

                var chapter = await _chapterServices.GetChapterByIdAsync(updateLessonModel.ChapterId.Value);
                var userIdFromToken = int.Parse(usernameFromToken);

                if (chapter.Course.UserId != userIdFromToken)
                {
                    return Unauthorized("You are not authorized to update this lesson.");
                }

                _unitOfWork.BeginTransaction();
                //  var lesson = _mapper.Map<Lesson>(updateLessonModel);

                await _lessonServices.UpdateLessonAsync(id, updateLessonModel);

                var updatedLesson = await _lessonServices.GetLessonByIdAsync(id, User);

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                return Ok(new LessonViewModel
                {
                    LessonId = updatedLesson.LessonId,
                    ChapterId = updatedLesson.ChapterId,
                    Title = updatedLesson.Title,
                    Content = updatedLesson.Content,
                    VideoURL = updatedLesson.VideoUrl,
                    CreateDate = updatedLesson.CreateDate,
                    Duration = updatedLesson.Duration,
                    OrderIndex = updatedLesson.OrderIndex,
                    Status = updatedLesson.Status
                });
            }
            catch (KeyNotFoundException ex)
            {
                _unitOfWork.RollbackTransaction();
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("view-lesson")]
        public async Task<IActionResult> ViewAcceptedLessons([FromQuery] List<string>? filterOn,
    [FromQuery] List<string>? filterQuery,
    [FromQuery] string? sortBy,
    [FromQuery] bool isAscending = true,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] int? filterDuration = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("User is not logged in.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User not authenticated.");

            try
            {
                _unitOfWork.BeginTransaction();

                List<FilterCriteria> filters = new List<FilterCriteria>();

                if (filterOn != null && filterQuery != null && filterOn.Count == filterQuery.Count)
                {
                    for (int i = 0; i < filterOn.Count; i++)
                    {
                        filters.Add(new FilterCriteria { FilterOn = filterOn[i], FilterQuery = filterQuery[i] });
                    }
                }
                var userIdInt = int.Parse(userId);
                var lessons = await _lessonServices.ViewActiveLessonsAsync(
                    userIdInt,
                    filters,
                    sortBy,
                    isAscending,
                    pageNumber,
                    pageSize,
                    filterDuration
                );

                var lessonViewModels = lessons.Select(lesson => new LessonViewModel
                {
                    LessonId = lesson.LessonId,
                    ChapterId = lesson.ChapterId,
                    Title = lesson.Title,
                    Content = lesson.Content,
                    VideoURL = lesson.VideoURL,
                    CreateDate = lesson.CreateDate,
                    Duration = lesson.Duration,
                    OrderIndex = lesson.OrderIndex,
                    Status = lesson.Status
                }).ToList();

                return Ok(lessonViewModels);

            }
            catch (InvalidOperationException ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete-lesson")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            try
            {

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("User is not logged in.");
                }

                var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userIdFromToken == null)
                {
                    return Unauthorized("Token does not contain a valid UserName.");
                }

                var existingLesson = await _lessonServices.GetLessonByIdAsync(id, User);

                if (existingLesson == null)
                {
                    throw new KeyNotFoundException($"Lesson with Id {id} not found.");
                }

                var chapter = await _chapterServices.GetChapterByIdAsync(existingLesson.ChapterId.Value);
                var userId = int.Parse(userIdFromToken);
                if (chapter == null || chapter.Course.UserId != userId)
                {
                    return Unauthorized("You are not authorized to delete this lesson.");
                }

                var deletedLesson = await _lessonServices.DeleteLessonAsync(id);
                return Ok(new LessonViewModel
                {
                    LessonId = deletedLesson.LessonId,
                    ChapterId = deletedLesson.ChapterId,
                    Title = deletedLesson.Title,
                    Content = deletedLesson.Content,
                    VideoURL = deletedLesson.VideoUrl,
                    CreateDate = deletedLesson.CreateDate,
                    Duration = deletedLesson.Duration,
                    OrderIndex = deletedLesson.OrderIndex,
                    Status = deletedLesson.Status
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        //[HttpGet("{id}")]
        //public async Task<ActionResult<BookmarkDetail>> GetLessonById(int id)
        //{
        //    var lesson = await _lessonServices.GetLessonByIdAsync(id, User);
        //    if (lesson == null)
        //        return NotFound();

        //    return Ok(_mapper.Map<LessonViewModel>(lesson));
        //}
    }
}