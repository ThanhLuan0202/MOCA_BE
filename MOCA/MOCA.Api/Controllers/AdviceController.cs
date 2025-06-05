using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA_Services.Interfaces;
using System.Security.Claims;

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdviceController : ControllerBase
    {
        private readonly IChatAdviceService _adviceService;

        public AdviceController(IChatAdviceService adviceService)
        {
            _adviceService = adviceService;
        }

        [HttpPost("advice")]
        [Authorize]
        public async Task<IActionResult> GetAdvice()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Không xác định được user.");

            var advice = await _adviceService.GetAdviceAsync(userId);
            return Ok(new { advice });
        }
    }
}
