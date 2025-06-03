using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Models.CommunityPostDTO;
using MOCA_Repositories.Models.CommunityReplyDTO;
using MOCA_Services.Interfaces;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityReplyController : ControllerBase
    {
        private readonly ICommunityReplyService _communityReplyService;
        public CommunityReplyController(ICommunityReplyService communityReplyService)
        {
            _communityReplyService = communityReplyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var communityReplies = await _communityReplyService.GetAllAsync();
            var result = communityReplies.Select(c => new ViewCommunityReplyModel
            {
                ReplyId = c.ReplyId,
                PostId = c.PostId,
                UserId = c.UserId,
                Content = c.Content,
                ParentReplyId = c.ParentReplyId,
                CreatedDate = c.CreatedDate
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var communityReply = await _communityReplyService.GetByIdAsync(id);
            if (communityReply == null) return NotFound();

            var result = new ViewCommunityReplyModel
            {
                ReplyId = communityReply.ReplyId,
                PostId = communityReply.PostId,
                UserId = communityReply.UserId,
                Content = communityReply.Content,
                ParentReplyId = communityReply.ParentReplyId,
                CreatedDate = communityReply.CreatedDate
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommunityReplyModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _communityReplyService.AddAsync(userId, model);

            var result = new ViewCommunityReplyModel
            {
                ReplyId = created.ReplyId,
                PostId = created.PostId,
                UserId = created.UserId,
                Content = created.Content,
                ParentReplyId = created.ParentReplyId,
                CreatedDate = created.CreatedDate
            };

            return CreatedAtAction(nameof(GetById), new { id = result.ReplyId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommunityReplyModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized("Invalid user ID.");

            var existing = await _communityReplyService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { message = $"CommunityReply with ID {id} not found." });

            if (existing.UserId != userId)
                return Forbid("You are not allowed to update this community reply.");

            var updated = await _communityReplyService.UpdateAsync(id, model);
            if (updated == null)
                return NotFound();

            var result = new ViewCommunityReplyModel
            {
                ReplyId = updated.ReplyId,
                PostId = updated.PostId,
                UserId = updated.UserId,
                Content = updated.Content,
                ParentReplyId = updated.ParentReplyId,
                CreatedDate = updated.CreatedDate
            };

            return Ok(result);
        }


        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized("User is not authenticated.");

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userIdInt))
                return Unauthorized("Invalid user ID.");

            var communityReply = await _communityReplyService.GetByIdAsync(id);
            if (communityReply == null)
                return NotFound(new { message = $"CommunityReply with ID {id} not found." });

            if (communityReply.UserId != userIdInt)
                return Forbid("You are not allowed to delete this community reply.");

            var deleted = await _communityReplyService.DeleteAsync(id);

            if (deleted == null)
                return NotFound(new { message = $"CommunityReply with ID {id} not found or already inactive." });

            return Ok(new
            {
                message = "CommunityReply deactivated successfully.",
                ReplyId = deleted.ReplyId,
                Status = deleted.Status
            });
        }
    }
}
