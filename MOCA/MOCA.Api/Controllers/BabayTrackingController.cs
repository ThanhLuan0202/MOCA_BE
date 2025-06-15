using Microsoft.AspNetCore.Mvc;
using MOCA_Repositories.Enitities;
using MOCA_Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MOCA.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BabayTrackingController : ControllerBase
    {
        private readonly IBabyTrackingService _service;


        public BabayTrackingController(IBabyTrackingService service)
        {
            _service = service;
        }



        // GET: api/<BabayTrackingController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BabyTracking>>> GetAllBabyTracking()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var query = await _service.GetAlLBabyTrackingAsync(userId);

            return Ok(query);
        }

        // GET api/<BabayTrackingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BabyTracking>> GetBabyTrackingById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var check = await _service.GetBabyTrackingByIdAsync(userId);

            return Ok(check);

        }

        // POST api/<BabayTrackingController>
        [HttpPost]
        public async Task<ActionResult<BabyTracking>> CreateBbTracking([FromBody] BabyTracking value)
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
            var newBbTracking = await _service.CreateBabyTrackingAsync(value, userId);

            return Ok(newBbTracking);
        }

        // PUT api/<BabayTrackingController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BabyTracking>> UpdateBbTracking(int id, [FromBody] BabyTracking value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bbUpdate = await _service.UpdateBabyTrackingAsync(id, value);

            return Ok(bbUpdate);
        }

        
    }
}
