using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackService.GetAllAsync();
            var result = feedbacks.Select(f => new ViewFeedbackModel
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                CourseId = f.CourseId,
                Rating = f.Rating,
                Comment = f.Comment,
                CreateDate = f.CreateDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _feedbackService.GetByIdAsync(id);
            if (feedback == null) return NotFound();

            var result = new ViewFeedbackModel
            {
                FeedbackId = feedback.FeedbackId,
                UserId = feedback.UserId,
                CourseId = feedback.CourseId,
                Rating = feedback.Rating,
                Comment = feedback.Comment,
                CreateDate = feedback.CreateDate
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFeedbackModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _feedbackService.AddAsync(userId, model);

            var result = new ViewFeedbackModel
            {
                FeedbackId = created.FeedbackId,
                UserId = created.UserId,
                CourseId = created.CourseId,
                Rating = created.Rating,
                Comment = created.Comment,
                CreateDate = created.CreateDate
            };

            return CreatedAtAction(nameof(GetById), new { id = result.FeedbackId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFeedbackModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _feedbackService.UpdateAsync(id, model);
            if (updated == null) return NotFound();

            var result = new ViewFeedbackModel
            {
                FeedbackId = updated.FeedbackId,
                UserId = updated.UserId,
                CourseId = updated.CourseId,
                Rating = updated.Rating,
                Comment = updated.Comment,
                CreateDate = updated.CreateDate
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _feedbackService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest(new { message = "Delete failed" });
            }

            return Ok(new { message = "Delete successful" });
        }


    }
}
