using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CommunityPost;
using MOCA_Repositories.Models.FeedbackDTO;
using MOCA_Services.Interfaces;
using MOCA_Services.Services;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityPostController : ControllerBase
    {
        private readonly ICommunityPostService _communityPostService;
        public CommunityPostController(ICommunityPostService communityPostService)
        {
            _communityPostService = communityPostService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var communityPosts = await _communityPostService.GetAllAsync();
            var result = communityPosts.Select(c => new ViewCommunityPostModel
            {
                PostId = c.PostId,
                UserId = c.UserId,
                Title = c.Title,
                Content = c.Content,
                Tags = c.Tags,
                ImageUrl = c.ImageUrl,
                Status = c.Status,
                CreateDate = c.CreateDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var communityPost = await _communityPostService.GetByIdAsync(id);
            if (communityPost == null) return NotFound();

            var result = new ViewCommunityPostModel
            {
                PostId = communityPost.PostId,
                UserId = communityPost.UserId,
                Title = communityPost.Title,
                Content = communityPost.Content,
                Tags = communityPost.Tags,
                ImageUrl = communityPost.ImageUrl,
                Status = communityPost.Status,
                CreateDate = communityPost.CreateDate
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommunityPostModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _communityPostService.AddAsync(userId, model);

            var result = new ViewCommunityPostModel
            {
                PostId = created.PostId,
                UserId = created.UserId,
                Title = created.Title,
                Content = created.Content,
                Tags = created.Tags,
                ImageUrl = created.ImageUrl,
                Status = created.Status,
                CreateDate = created.CreateDate
            };

            return CreatedAtAction(nameof(GetById), new { id = result.PostId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommunityPostModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user ID.");

            var existing = await _communityPostService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"CommunityPost with ID {id} not found." });

            if (existing.UserId != userId)
                return Forbid("You are not allowed to update this community post.");

            var updated = await _communityPostService.UpdateAsync(id, model);
            if (updated == null)
                return NotFound();

            var result = new ViewCommunityPostModel
            {
                PostId = updated.PostId,
                UserId = updated.UserId,
                Title = updated.Title,
                Content = updated.Content,
                Tags = updated.Tags,
                ImageUrl = updated.ImageUrl,
                Status = updated.Status,
                CreateDate = updated.CreateDate
            };

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userIdInt))
                return Unauthorized("Invalid user ID.");

            var communityPost = await _communityPostService.GetByIdAsync(id);
            if (communityPost == null)
                return NotFound(new { message = $"CommunityPost with ID {id} not found." });

            if (communityPost.UserId != userIdInt)
                return Forbid("You are not allowed to delete this community post.");

            var deleted = await _communityPostService.DeleteAsync(id);

            if (deleted == null)
                return NotFound(new { message = $"CommunityPost with ID {id} not found or already inactive." });

            return Ok(new
            {
                message = "CommunityPost deactivated successfully.",
                PostId = deleted.PostId,
                Status = deleted.Status
            });
        }
    }
}
