using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.PostLikeDTO;
using MOCA_Services.Interfaces;
using MOCA_Services.Services;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private readonly IPostLikeService _postLikeService;
        public PostLikeController(IPostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var postLikes = await _postLikeService.GetAllAsync();
            var result = postLikes.Select(p => new ViewPostLikeModel
            {
                LikeId = p.LikeId,
                PostId = p.PostId,
                UserId = p.UserId,
                CreatedDate = p.CreatedDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var postLike = await _postLikeService.GetByIdAsync(id);
            if (postLike == null) return NotFound();

            var result = new ViewPostLikeModel
            {
                LikeId = postLike.LikeId,
                PostId = postLike.PostId,
                UserId = postLike.UserId,
                CreatedDate = postLike.CreatedDate
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostLikeModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _postLikeService.AddAsync(userId, model);

            var result = new ViewPostLikeModel
            {
                LikeId = created.LikeId,
                PostId = created.PostId,
                UserId = created.UserId,
                CreatedDate = created.CreatedDate
            };

            return CreatedAtAction(nameof(GetById), new { id = result.LikeId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userIdInt))
                return Unauthorized("Invalid user ID.");

            var postLike = await _postLikeService.GetByIdAsync(id);
            if (postLike == null)
                return NotFound(new { message = $"Post Like with ID {id} not found." });

            if (postLike.UserId != userIdInt)
                return Forbid("You are not allowed to delete this post like.");

            var deleted = await _postLikeService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest(new { message = "Delete failed" });
            }

            return Ok(new { message = "Delete successful" });
        }
    }
}
