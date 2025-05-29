using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.ChapterDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterServices _chapterServices;
        private readonly ICourseServices _courseServices;
        private readonly IUnitOfWork _unitOfWork;

        public ChapterController(IChapterServices chapterServices, IUnitOfWork unitOfWork, ICourseServices courseServices)
        {
            _chapterServices = chapterServices;
            _unitOfWork = unitOfWork;
            _courseServices = courseServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterViewModel>> GetById(int id)
        {
            var chapter = await _chapterServices.GetChapterByIdAsync(id);
            if (chapter == null)
                return NotFound();
            var chapterViewModel = new ChapterViewModel
            {
                ChapterId = chapter.ChapterId,
                CourseId = chapter.CourseId,
                Title = chapter.Title,
                OrderIndex = chapter.OrderIndex,
                Status = chapter.Status
            };
            return Ok(chapterViewModel);
        }

        [HttpPost("add-chapter")]
        public async Task<IActionResult> AddChapters([FromBody] List<AddChapterModel> addChapterModels)
        {

            ConvertDefaultValuesToNull(addChapterModels);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User is not Logged In!.");
            }

            try
            {
                _unitOfWork.BeginTransaction();

                var userIdInt = int.Parse(userId);

                var addedChapters = await _chapterServices.AddChaptersAsync(addChapterModels, userIdInt);
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                var addedViewModels = addedChapters.Select(ch => new AddChapterModel
                {
                    CourseId = ch.CourseId,
                    Title = ch.Title,
                    OrderIndex = ch.OrderIndex,
                }).ToList();

                return Ok(addedViewModels);
            }
            catch (KeyNotFoundException ex)
            {
                _unitOfWork.RollbackTransaction();
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex.Message);
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

        [HttpPut("update-chapter")]
        public async Task<IActionResult> UpdateChapter([Required] int id, [FromForm] UpdateChapterModel updateChapterModel)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("User is not logged in.");
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return Unauthorized("User not authenticated.");

                var existingChapter = await _chapterServices.GetChapterByIdAsync(id);

                if (existingChapter == null)
                {
                    throw new KeyNotFoundException($"Chapter with Id {id} not found.");
                }

                _unitOfWork.BeginTransaction();
                var userIdInt = int.Parse(userId);
                await _chapterServices.UpdateChapterAsync(id, updateChapterModel, userIdInt);

                var updatedChapter = await _chapterServices.GetChapterByIdAsync(id);

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();

                var chapterViewModel = new ChapterViewModel
                {
                    ChapterId = updatedChapter.ChapterId,
                    CourseId = updatedChapter.CourseId,
                    Title = updatedChapter.Title,
                    OrderIndex = updatedChapter.OrderIndex,
                    Status = updatedChapter.Status
                };

                return Ok(chapterViewModel);
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

        [HttpGet("view-chapter")]
        public async Task<IActionResult> ViewAcceptedChapters(string? searchContent, string? sortBy, bool ascending, int? pageNumber, int? pageSize, int? filterDuration)
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

                var userIdInt = int.Parse(userId);

                var chapters = await _chapterServices.ViewActiveChaptersAsync(userIdInt, searchContent, sortBy, ascending, pageNumber, pageSize );
                var chapterViewModels = chapters.Select(ch => new ChapterViewModel
                {
                    ChapterId = ch.ChapterId,
                    CourseId = ch.CourseId,
                    Title = ch.Title,
                    OrderIndex = ch.OrderIndex,
                    Status = ch.Status
                }).ToList();

                return Ok(chapterViewModels);
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

        [HttpDelete("delete-chapter")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            try
            {

                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("User is not logged in.");
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return Unauthorized("User not authenticated.");

                var userIdInt = int.Parse(userId);
                var existingChapter = await _chapterServices.GetChapterByIdAsync(id);

                if (existingChapter == null)
                {
                    throw new KeyNotFoundException($"Chapter with Id {id} not found.");
                }

                var course = await _courseServices.GetByIdAllStatusAsync(existingChapter.CourseId.Value);

                if (course == null || course.UserId != userIdInt)
                {
                    return Unauthorized("You are not authorized to delete this chapter.");
                }

                var deletedChapter = await _chapterServices.DeleteChapterAsync(id);
                var chapterViewModel = new ChapterViewModel
                {
                    ChapterId = deletedChapter.ChapterId,
                    CourseId = deletedChapter.CourseId,
                    Title = deletedChapter.Title,
                    OrderIndex = deletedChapter.OrderIndex,
                    Status = deletedChapter.Status
                };

                return Ok(chapterViewModel);
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

        private void ConvertDefaultValuesToNull(List<AddChapterModel> addChapterModels)
        {
            foreach (var model in addChapterModels)
            {
                if (model.Title == "string")
                    model.Title = null;

                if (model.CourseId == 0)
                    model.CourseId = null;

                if (model.OrderIndex == 0)
                    model.OrderIndex = null;

            }
        }
    }
}
