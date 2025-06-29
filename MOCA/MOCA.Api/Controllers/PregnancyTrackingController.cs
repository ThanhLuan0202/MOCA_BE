using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Repositories.Interfaces;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Authorize(Roles = "Mom,Doctor,Manager,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PregnancyTrackingController : ControllerBase
    {

        private readonly IPregnancyTrackingService _service;

        public PregnancyTrackingController(IPregnancyTrackingService service)
        {
            _service = service;
        }
        // GET: api/<PregnancyTrackingController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PregnancyTracking>>> GetAllPregnancyTracking()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }
                var query = await _service.GetAlLPregnancyTrackingAsync(userId);
                return Ok(query);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        // GET api/<PregnancyTrackingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PregnancyTracking>>> GetPregnancyTrackingById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            var PreTrack = await _service.GetPregnancyTrackingByIdAsync(id);

            return Ok(PreTrack);    

        }

        // POST api/<PregnancyTrackingController>
        [HttpPost]
        public async Task<ActionResult<PregnancyTracking>> CreatePregnancyTracking([FromBody] PregnancyTracking value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var newPreTrack = await _service.CreatePregnancyTrackingAsync(value, userId);

            return Ok(newPreTrack);

        }

        // PUT api/<PregnancyTrackingController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PregnancyTracking>> UpdatePregnancyTracking(int id, [FromBody] PregnancyTracking value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatePreTrack = await _service.UpdatePregnancyTrackingAsync(id, value);

            return Ok(updatePreTrack);
        }
        [HttpGet("GetPregnancyTrackingByUserId{id}")]
        public async Task<ActionResult<IEnumerable<PregnancyTracking>>> GetPregnancyTrackingByUserId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PreTrack = await _service.GetPregnancyTrackingByUserId(id);

            return Ok(PreTrack);

        }
        
    }
}
